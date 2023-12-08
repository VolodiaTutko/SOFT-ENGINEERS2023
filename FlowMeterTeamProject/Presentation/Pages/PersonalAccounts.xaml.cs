﻿using DAL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Text.RegularExpressions;
using DAL.Data.DataMock;
using Presentation.PersonalAccountDialogWindow;
using BLL.Utils.DataGrid;
using FlowMeterTeamProject.Presentation.DialogWindows;

namespace Presentation.Pages
{
    /// <summary>
    /// Interaction logic for PersonalAccounts.xaml
    /// </summary>
    public partial class PersonalAccounts : Page
    {
        public PersonalAccounts()
        {
            InitializeComponent();

            FillDataGrid();

            dataGrid.MouseRightButtonDown += DataGrid_MouseRightButtonDown;
        }

        public void FillDataGrid()
        {
            using (var context = new AppDbContext())
            {
                List<Consumer> consumers = context.consumers.ToList();

                DataTable dt = new DataTable("Consumer");
                dt.Columns.Add("№", typeof(int));
                dt.Columns.Add("ConsumersId", typeof(int));
                dt.Columns.Add("PersonalAccount", typeof(string));
                dt.Columns.Add("Flat", typeof(int));
                dt.Columns.Add("ConsumerOwner", typeof(string));
                dt.Columns.Add("HeatingArea", typeof(int));

                for (int i = 0; i < consumers.Count; i++)
                {
                    dt.Rows.Add(
                        i + 1,
                        consumers[i].ConsumersId,
                        consumers[i].PersonalAccount,
                        consumers[i].Flat,
                        consumers[i].ConsumerOwner,
                        consumers[i].HeatingArea
                    );
                }

                dt.Columns["№"].SetOrdinal(0);

                dataGrid.ItemsSource = dt.DefaultView;

            }
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

                if (selectedRowsData.Count > 0)
                {
                    RowDetails dialog = new RowDetails(selectedRowsData);
                    dialog.Owner = Window.GetWindow(this);
                    dialog.ShowDialog();
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


        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            XlsxExporter.ExportToExcelButton_Click(sender, e, dataGrid);
        }

        private void ExportToPdfButton_Click(object sender, RoutedEventArgs e)
        {
            PdfExporter.ExportToPdfButton_Click(sender, e, dataGrid);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridSearch.PerformSearch(dataGrid, searchBox);
        }

        private void AddNewRecordButton_Click(object sender, RoutedEventArgs e)
        {
            var propertiesAccountsWindow = new Presentation.PersonalAccountDialogWindow.PropertiesAccounts();
            propertiesAccountsWindow.ShowDialog();
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
