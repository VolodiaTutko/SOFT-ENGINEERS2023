using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace FlowMeterTeamProject.Utils.DataGrid
{
    internal class PdfExporter
    {
        public static void ExportToPdfButton_Click(object sender, RoutedEventArgs e, System.Windows.Controls.DataGrid dataGrid)
        {
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = Path.Combine(downloadsPath, "ExportedData.pdf");
            ExportDataGridToPdf(dataGrid, filePath);
        }

        private static void ExportDataGridToPdf(System.Windows.Controls.DataGrid dataGrid, string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    using (iText.Kernel.Pdf.PdfWriter writer = new iText.Kernel.Pdf.PdfWriter(fs))
                    {
                        using (iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(writer))
                        {
                            Document document = new Document(pdf);

                            document.Add(new iText.Layout.Element.Paragraph("Exported DataGrid to PDF"));

                            float[] columnWidths = { 2, 3, 3, 3, 3, 3, 3 };
                            iText.Layout.Element.Table table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(columnWidths));

                            table.SetFontSize(8);

                            foreach (DataGridColumn column in dataGrid.Columns)
                            {
                                if (!(column is DataGridTemplateColumn && ((DataGridTemplateColumn)column).CellTemplate.LoadContent() is CheckBox))
                                {
                                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(column.Header.ToString())));
                                }
                            }

                            foreach (var item in dataGrid.Items)
                            {
                                if (item is DataRowView rowView && rowView.Row.ItemArray.Length > 0)
                                {
                                    var row = rowView.Row;
                                    foreach (DataGridColumn column in dataGrid.Columns)
                                    {
                                        if (!(column is DataGridTemplateColumn && ((DataGridTemplateColumn)column).CellTemplate.LoadContent() is CheckBox))
                                        {
                                            string propertyName = column.Header.ToString();
                                            object cellValue = row[propertyName];
                                            table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(cellValue?.ToString())));
                                        }
                                    }
                                    table.StartNewRow();
                                }
                            }

                            document.Add(table);
                        }
                    }
                }

                MessageBox.Show("Data exported to PDF successfully.", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data to PDF: {ex.Message}", "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
