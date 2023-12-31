﻿namespace BLL.Utils.DataGrid
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
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
    using DAL.Data;
    using OfficeOpenXml;

    internal class XlsxExporter
    {
        public static void ExportToExcelButton_Click(object sender, RoutedEventArgs e, System.Windows.Controls.DataGrid dataGrid, List<string> customHeaders)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string downloadsPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = System.IO.Path.Combine(downloadsPath, $"ExportedData_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            ExportDataGridToExcel(dataGrid, filePath, customHeaders);
        }

        private static void ExportDataGridToExcel(System.Windows.Controls.DataGrid dataGrid, string filePath, List<string> customHeaders)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("ExportedData");

                    int headerIndex = 1;

                    Dictionary<string, string> columnHeaderToPropertyName = new Dictionary<string, string>();

                    foreach (DataGridColumn column in dataGrid.Columns)
                    {
                        if (!(column is DataGridTemplateColumn && ((DataGridTemplateColumn)column).CellTemplate.LoadContent() is CheckBox))
                        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                            string columnHeader = column.Header.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                            string propertyName = columnHeader.Replace(" ", string.Empty);

                            columnHeaderToPropertyName[columnHeader] = propertyName;

                            worksheet.Cells[1, headerIndex].Value = columnHeader;
                            headerIndex++;
                        }
                    }

                    int rowIndex = 2;

                    bool checkboxesSelected = false;

                    foreach (var item in dataGrid.Items)
                    {
                        DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

                        if (row != null)
                        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                            CheckBox checkBox = FindVisualChild<CheckBox>(row);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                            if (checkBox != null && checkBox.IsChecked == true)
                            {
                                checkboxesSelected = true;
                                break;
                            }
                        }
                    }

                    foreach (var item in dataGrid.Items)
                    {
                        if (!checkboxesSelected || (checkboxesSelected && IsRowSelected(item, dataGrid)))
                        {
                            if (item is DataRowView rowView && rowView.Row.ItemArray.Length > 0)
                            {
                                var row = rowView.Row;

                                bool startNewLine = headerIndex > 1;

                                int columnIndex = 1;

                                foreach (var header in customHeaders)
                                {
                                    if (startNewLine)
                                    {
                                        worksheet.Cells[rowIndex, columnIndex].Value = row[header]?.ToString();
                                    }
                                    else
                                    {
                                        worksheet.Cells[rowIndex, columnIndex].Value = header;
                                        startNewLine = true;
                                    }

                                    columnIndex++;
                                }

                                rowIndex++;
                            }
                        }
                    }

                    package.SaveAs(new FileInfo(filePath));
                }

                MessageBox.Show($"Дані успішно експортовані в Excel в папку {Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads"}", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при експортуванні даних в Excel: {ex.Message}", "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private static T? FindVisualChild<T>(DependencyObject obj)
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                T childOfChild = FindVisualChild<T>(child);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }

            return null;
        }

        private static bool IsRowSelected(object item, System.Windows.Controls.DataGrid dataGrid)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

            if (row != null)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                CheckBox checkBox = FindVisualChild<CheckBox>(row);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                return checkBox != null && checkBox.IsChecked == true;
            }

            return false;
        }
    }
}
