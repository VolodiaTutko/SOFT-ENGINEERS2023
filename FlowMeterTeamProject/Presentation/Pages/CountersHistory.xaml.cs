using BLL.Utils.DataGrid;
using FlowMeterTeamProject.BLL.Features.Counters;
using FlowMeterTeamProject.Presentation.Features.Counters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowMeterTeamProject.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for CountersHistory.xaml
    /// </summary>
    public partial class CountersHistory : Page, IDataGridUpdater
    {
        private CountersInfo countersInfo;
        public CountersHistory()
        {
            InitializeComponent();
            this.countersInfo = new CountersInfo();
            currentDateTextBlock.Text = DateTime.Now.ToString("dd-MM-yyyy");

            FillDataGrid();
        }

        public event EventHandler DataGridUpdated;

        public void UpdateDataGrid()
        {
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            List<CounterRecord> counters = countersInfo.GetCounterRecords();

            DataTable dt = new DataTable("Counter");
            dt.Columns.Add("№", typeof(int));
            dt.Columns.Add("Account", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Current Value", typeof(decimal));
            dt.Columns.Add("Value as of date", typeof(string));

            for (int i = 0; i < counters.Count; i++)
            {
                dt.Rows.Add(
                    i + 1,
                    counters[i].Account,
                    counters[i].TypeOfAccount,
                    counters[i].CurrentValue,
                    counters[i].LastModified.ToString("dd/MM/yyyy")
                );
            }

            dt.Columns["№"].SetOrdinal(0);

            dataGrid.ItemsSource = dt.DefaultView;
        }


        List<string> customHeaders = new List<string>
        {
            "№",
            "Account",
            "Type",
            "Current Value",
            "Value as of date"
        };

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            XlsxExporter.ExportToExcelButton_Click(sender, e, dataGrid, customHeaders);
        }

        private void ExportToPdfButton_Click(object sender, RoutedEventArgs e)
        {
            PdfExporter.ExportToPdfButton_Click(sender, e, dataGrid, "Інформація по лічильниках", customHeaders);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridSearch.PerformSearch(dataGrid, searchBox);
        }

        private void CheckBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            DataGridRow dataGridRow = FindAncestor<DataGridRow>(checkBox);
            if (dataGridRow != null)
            {
                dataGridRow.IsSelected = !dataGridRow.IsSelected;
            }
            e.Handled = true;
        }

        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T ancestor)
                {
                    return ancestor;
                }
                current = VisualTreeHelper.GetParent(current);
            } while (current != null);

            return null;
        }

        private void SelectAllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectAllCheckBoxes(true);
        }

        private void SelectAllCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SelectAllCheckBoxes(false);
        }

        private void SelectAllCheckBoxes(bool isChecked)
        {
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                var dataGridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);

                if (dataGridRow != null)
                {
                    var checkBox = FindCheckBoxInVisualTree(dataGridRow);
                    if (checkBox != null)
                    {
                        checkBox.IsChecked = isChecked;
                    }
                }
            }
        }

        private CheckBox FindCheckBoxInVisualTree(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is CheckBox checkBox)
                {
                    return checkBox;
                }

                var result = FindCheckBoxInVisualTree(child);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }


        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            AddNewCounter newCounterDialog = new AddNewCounter(this);
            newCounterDialog.Show();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
