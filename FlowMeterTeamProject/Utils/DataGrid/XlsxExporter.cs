using FlowMeterTeamProject.Data;
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
using System.IO;
using OfficeOpenXml;

namespace FlowMeterTeamProject.Utils.DataGrid
{
    internal class XlsxExporter
    {
        public static void ExportToExcelButton_Click(object sender, RoutedEventArgs e, System.Windows.Controls.DataGrid dataGrid)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string downloadsPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = System.IO.Path.Combine(downloadsPath, "ExportedDataXLSX.xlsx");
            ExportDataGridToExcel(dataGrid, filePath);
        }

        private static void ExportDataGridToExcel(System.Windows.Controls.DataGrid dataGrid, string filePath)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("ExportedData");

                    int headerIndex = 1;
                    foreach (DataGridColumn column in dataGrid.Columns)
                    {
                        if (!(column is DataGridTemplateColumn && ((DataGridTemplateColumn)column).CellTemplate.LoadContent() is CheckBox))
                        {
                            worksheet.Cells[1, headerIndex].Value = column.Header.ToString();
                            headerIndex++;
                        }
                    }

                    int rowIndex = 2;

                    // Check if any checkboxes are selected
                    bool checkboxesSelected = false;

                    foreach (var item in dataGrid.Items)
                    {
                        DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

                        if (row != null)
                        {
                            CheckBox checkBox = FindVisualChild<CheckBox>(row);

                            if (checkBox != null && checkBox.IsChecked == true)
                            {
                                checkboxesSelected = true;
                                break;
                            }
                        }
                    }

                    // Export all rows or only selected rows based on checkboxesSelected
                    foreach (var item in dataGrid.Items)
                    {
                        if (!checkboxesSelected || (checkboxesSelected && IsRowSelected(item, dataGrid)))
                        {
                            if (item is DataRowView rowView && rowView.Row.ItemArray.Length > 0)
                            {
                                var row = rowView.Row;
                                int columnIndex = 1;
                                foreach (DataGridColumn column in dataGrid.Columns)
                                {
                                    if (!(column is DataGridTemplateColumn && ((DataGridTemplateColumn)column).CellTemplate.LoadContent() is CheckBox))
                                    {
                                        string propertyName = column.Header.ToString();
                                        object cellValue = row[propertyName];
                                        worksheet.Cells[rowIndex, columnIndex].Value = cellValue?.ToString();
                                        columnIndex++;
                                    }
                                }
                                rowIndex++;
                            }
                        }
                    }

                    package.SaveAs(new FileInfo(filePath));
                }

                MessageBox.Show("Data exported to Excel successfully.", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data to Excel: {ex.Message}", "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is T)
                    return (T)child;

                T childOfChild = FindVisualChild<T>(child);

                if (childOfChild != null)
                    return childOfChild;
            }

            return null;
        }

        private static bool IsRowSelected(object item, System.Windows.Controls.DataGrid dataGrid)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

            if (row != null)
            {
                CheckBox checkBox = FindVisualChild<CheckBox>(row);

                return checkBox != null && checkBox.IsChecked == true;
            }

            return false;
        }
    }
}
