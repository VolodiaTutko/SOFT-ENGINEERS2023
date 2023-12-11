using DAL.Data;
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
using Presentation.HousesDialogWindow;
using FlowMeterTeamProject.Presentation;
using BLL.Utils.DataGrid;
using FlowMeterTeamProject.Presentation.DialogWindows;
using FlowMeterTeamProject.Presentation.PersonalAccountDialogWindow;
using Presentation.PersonalAccountDialogWindow;

namespace Presentation.Pages
{
    /// <summary>
    /// Interaction logic for Houses.xaml
    /// </summary>
    public partial class Houses : Page, IDataGridUpdater
    {

        public int houseID;
        public Houses()
        {
            InitializeComponent();
            FillDataGrid();



            
        }

        public event EventHandler DataGridUpdated;

        public void UpdateDataGrid()
        {
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            using (var context = new AppDbContext())
            {
                List<House> houses = context.houses.ToList();

                DataTable dt = new DataTable("House");
                dt.Columns.Add("№", typeof(int));
                dt.Columns.Add("HouseAddress", typeof(string));
                dt.Columns.Add("HeatingAreaOfHouse", typeof(int));
                dt.Columns.Add("NumberOfFlat", typeof(int));
                dt.Columns.Add("NumberOfResidents", typeof(int));


                for (int i = 0; i < houses.Count; i++)
                {
                    dt.Rows.Add(i + 1, houses[i].HouseAddress, houses[i].HeatingAreaOfHouse, houses[i].NumberOfFlat, houses[i].NumberOfResidents);
                }

                dt.Columns["№"].SetOrdinal(0);

                dataGrid.ItemsSource = dt.DefaultView;
            }
        }

        List<string> customHeaders = new List<string>
        {
            "№",
            "HouseAddress",
            "HeatingAreaOfHouse",
            "NumberOfFlat",
            "NumberOfResidents"
        };


        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            XlsxExporter.ExportToExcelButton_Click(sender, e, dataGrid, customHeaders);
        }

        private void ExportToPdfButton_Click(object sender, RoutedEventArgs e)
        {
            PdfExporter.ExportToPdfButton_Click(sender, e, dataGrid, "Інформація по будинках", customHeaders);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridSearch.PerformSearch(dataGrid, searchBox);
        }

        private void AddNewRecordButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewAccount = new AddNewAccount(this);
            addNewAccount.ShowDialog();
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewHouse newDialog = new AddNewHouse(this);
            newDialog.Show();
        }

        private void DataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedRows = dataGrid.SelectedItems;

            if (selectedRows.Count > 0)
            {
                List<Dictionary<string, string>> selectedRowsData = new List<Dictionary<string, string>>();

                foreach (var selectedItem in selectedRows)
                {
                    var row = (DataGridRow)(dataGrid.ItemContainerGenerator.ContainerFromItem(selectedItem));

                    if (row != null)
                    {
                        CheckBox checkBox = FindVisualChild<CheckBox>(row);

                        if (checkBox != null && checkBox.IsChecked == true)
                        {
                            Dictionary<string, string> rowData = new Dictionary<string, string>();

                            foreach (var column in dataGrid.Columns)
                            {
                                if (column is DataGridTextColumn textColumn)
                                {
                                    string header = textColumn.Header.ToString();

                                    var cellContent = column.GetCellContent(row);

                                    if (cellContent is TextBlock textBlock)
                                    {
                                        string cellValue = textBlock.Text;
                                        rowData.Add(header, cellValue);
                                    }
                                }
                            }

                            selectedRowsData.Add(rowData);
                        }
                    }
                }

                if (selectedRowsData.Count > 1) 
                {
                    MessageBox.Show("Вибрано більше ніж один запис.");

                }
                else if (selectedRowsData.Count == 1)
                {

                    string houseAddress = selectedRowsData[0]["Адреса будинку"];

                    try
                    {
                        using (var context = new AppDbContext())
                        {
                            var house = context.houses.FirstOrDefault(c => c.HouseAddress == houseAddress);

                            houseID = house.HouseId;

                            context.SaveChanges();
                           
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при видаленні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                   
                    var properties = new PropertiesHouse(houseID, this);

                    properties.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select the checkbox in the row before viewing details.");
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);

                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }

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


    }
}
