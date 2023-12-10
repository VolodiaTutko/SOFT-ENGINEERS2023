using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Data.SqlClient;
using System.Net;
using DAL.Data;
using BLL.Utils.DataGrid;
using FlowMeterTeamProject.Presentation.DialogWindows;
using FlowMeterTeamProject.Presentation.PersonalAccountDialogWindow;
using Presentation.PersonalAccountDialogWindow;

namespace Presentation.Pages
{
    /// <summary>
    /// Interaction logic for Counters.xaml
    /// </summary>
    public partial class Counters : Page
    {
        public Counters()
        {
            InitializeComponent();
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            using (var context = new AppDbContext())
            {
                List<Counter> counters = context.counters.ToList();

                DataTable dt = new DataTable("Counter");
                dt.Columns.Add("№", typeof(int));
                dt.Columns.Add("CurrentIndicator", typeof(decimal));
                dt.Columns.Add("Account", typeof(string));
                dt.Columns.Add("TypeOfAccount", typeof(string));
                dt.Columns.Add("Date", typeof(DateTime));

                for (int i = 0; i < counters.Count; i++)
                {
                    dt.Rows.Add(
                        i + 1,
                        counters[i].CurrentIndicator,
                        counters[i].Account,
                        counters[i].TypeOfAccount,
                        counters[i].Date
                    );
                }

                dt.Columns["№"].SetOrdinal(0);

                dataGrid.ItemsSource = dt.DefaultView;
            }
        }

        List<string> customHeaders = new List<string>
        {
            "№",
            "CurrentIndicator",
            "Account",
            "TypeOfAccount",
            "Date"
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
    }
}