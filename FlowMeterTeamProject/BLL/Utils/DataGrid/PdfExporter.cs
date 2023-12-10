namespace BLL.Utils.DataGrid
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using iText.IO.Font;
    using iText.Kernel.Font;
    using iText.Kernel.Pdf;
    using iText.Layout;
    using iText.Layout.Element;
    using iText.Layout.Properties;

    internal class PdfExporter
    {
        public static void ExportToPdfButton_Click(object sender, RoutedEventArgs e, System.Windows.Controls.DataGrid dataGrid, string title, List<string> customHeaders)
        {
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = Path.Combine(downloadsPath, $"ExportedData_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            ExportDataGridToPdf(dataGrid, filePath, title, customHeaders);
        }

        private static void ExportDataGridToPdf(System.Windows.Controls.DataGrid dataGrid, string filePath, string title, List<string> customHeaders)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    using (PdfWriter writer = new PdfWriter(fs))
                    {
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            iText.Layout.Document document = new iText.Layout.Document(pdf);

                            document.Add(new iText.Layout.Element.Paragraph(title));

                            float[] columnWidths = Enumerable.Repeat(3, customHeaders.Count).Select(x => (float)x).ToArray();
                            Table table = new Table(UnitValue.CreatePercentArray(columnWidths));

                            PdfFont font = PdfFontFactory.CreateFont("Presentation/Assets/arial-unicode-ms.ttf", PdfEncodings.IDENTITY_H);
                            table.SetFontSize(8).SetFont(font);
                            document.Add(new iText.Layout.Element.Paragraph(title).SetFont(font));

                            foreach (DataGridColumn column in dataGrid.Columns)
                            {
                                if (!(column is DataGridTemplateColumn && ((DataGridTemplateColumn)column).CellTemplate.LoadContent() is CheckBox))
                                {
                                    string columnHeader = column.Header.ToString();
                                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(columnHeader)));
                                }
                            }

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
                                        var rowData = rowView.Row;

                                        bool startNewLine = table.GetNumberOfColumns() > 0;

                                        foreach (DataGridColumn column in dataGrid.Columns)
                                        {
                                            if (!(column is DataGridTemplateColumn && ((DataGridTemplateColumn)column).CellTemplate.LoadContent() is CheckBox))
                                            {
                                                Binding? binding = (column as DataGridBoundColumn)?.Binding as Binding;
                                                string? bindingPath = binding?.Path.Path;

                                                string columnHeader = customHeaders.FirstOrDefault(h => h.Equals(bindingPath, StringComparison.OrdinalIgnoreCase)) ?? bindingPath;

                                                if (startNewLine)
                                                {
                                                    table.StartNewRow();
                                                    startNewLine = false;
                                                }

                                                object cellValue = rowData[columnHeader];
                                                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(cellValue?.ToString())));
                                            }
                                        }
                                    }
                                }
                            }

                            document.Add(table);
                        }
                    }
                }

                MessageBox.Show("Дані успішно експортовані в PDF.", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при експортуванні даних в PDF: {ex.Message}", "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

    }
}
