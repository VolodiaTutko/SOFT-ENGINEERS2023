using DAL.Data;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO.Compression;


namespace FlowMeterTeamProject.BLL.Utils.DataGrid
{
    public class RowDetails
    {
        private readonly List<Dictionary<string, string>> rowsData;
        private List<string> pdfFilePaths;

        public RowDetails(List<Dictionary<string, string>> rowsData)
        {
            this.rowsData = rowsData;
            this.pdfFilePaths = new List<string>();
            int rowCount = 0;

            using (var dbContext = new AppDbContext())
            {
                foreach (var rowData in rowsData)
                {
                    Dictionary<string, List<(string ServiceName, string AccountNumber, decimal Price, decimal Tariff)>> finalsublist = new Dictionary<string, List<(string, string, decimal, decimal)>>(); 

                    foreach (var pair in rowData)
                    {
                        if (pair.Key == "Особовий рахунок")
                        {
                            string houseAddressValue;
                            _ = rowData.TryGetValue("Адреса будинку", out houseAddressValue);
                            finalsublist = ReceiptsLogic.GetNonZeroServices(pair.Value, houseAddressValue);
                            Dictionary<string, List<object>> newPair = new Dictionary<string, List<object>>();
                        }
                    }

                    CreatePdf(rowData, finalsublist);

                    rowCount++;
                }
            }

            // Check if there are more than one PDF file
            if (pdfFilePaths.Count > 1)
            {
                CreateZipFile();
            }
        }

        private void CreatePdf(Dictionary<string, string> rowData, Dictionary<string, List<(string, string, decimal, decimal)>> finalsublist)
        {
            string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string pdfFileName = $"receipt_{currentDateTime}.pdf";
            string pdfFilePath = Path.Combine(downloadsFolder, pdfFileName);

            using (var writer = new PdfWriter(pdfFilePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    // Use a custom font for the document
                    PdfFont font = PdfFontFactory.CreateFont("Presentation/Assets/arial-unicode-ms.ttf", PdfEncodings.IDENTITY_H);
                    document.SetFont(font);

                    string personalAccount = rowData.GetValueOrDefault("Особовий рахунок", "");
                    string street = rowData.GetValueOrDefault("Адреса будинку", "");
                    string apartment = rowData.GetValueOrDefault("Квартира", "");
                    string heatingArea = rowData.GetValueOrDefault("Опалювальна площа", "");
                    string residents = rowData.GetValueOrDefault("Кількість мешканців", "");
                    string fullName = rowData.GetValueOrDefault("Власник квартири", "");


                    document.Add(new Paragraph($"Отримувач: {personalAccount}"));
                    // Custom text
                    document.Add(new Paragraph("РАХУНОК НА СПЛАТУ ЗА ЖИТЛОВО-КОМУНАЛЬНІ ТА ІНШІ ПОСЛУГИ"));
                    document.Add(new Paragraph($"Вулиця: {street}, Квартира: {apartment}"));

                    // Create a table with 3 columns
                    Table consumerTable = new Table(3).UseAllAvailableWidth();

                    // Add header row
                    consumerTable.AddHeaderCell("Опалювальна площа");
                    consumerTable.AddHeaderCell("Проживає осіб");
                    consumerTable.AddHeaderCell("ПІБ");

                    // Add data row
                    consumerTable.AddCell(heatingArea);
                    consumerTable.AddCell(residents);
                    consumerTable.AddCell(fullName);

                    // Add the table to the document
                    document.Add(consumerTable);
                    document.Add(new Paragraph("\n"));





                    foreach (var kvp in finalsublist)
                    {
                        // Create the first table with 3 columns
                        Table table = new Table(4).UseAllAvailableWidth();

                        // Add header row
                        table.AddHeaderCell("Найменування послуги");
                        table.AddHeaderCell("Тарифи");
                        table.AddHeaderCell("Рахунок");
                        table.AddHeaderCell("До сплати");

                        foreach (var tuple in kvp.Value)
                        {
                            // Replace English names with Ukrainian equivalents
                            string serviceName = "";
                            switch (tuple.Item1)
                            {
                                case "HotWater":
                                    serviceName = "Гаряча вода";
                                    break;
                                case "ColdWater":
                                    serviceName = "Холодна вода";
                                    break;
                                case "Heating":
                                    serviceName = "Опалення";
                                    break;
                                case "Electricity":
                                    serviceName = "Електроенергія";
                                    break;
                                case "PublicService":
                                    serviceName = "Комунальні послуги";
                                    break;
                                default:
                                    serviceName = tuple.Item1;
                                    break;
                            }

                            // Add data rows with Ukrainian names
                            table.AddCell(serviceName);
                            table.AddCell(tuple.Item4.ToString());
                            table.AddCell(tuple.Item2);
                            table.AddCell(tuple.Item3.ToString());
                        }

                        document.Add(table);

                        // Add space between tables
                        document.Add(new Paragraph("\n"));

                        // Calculate the sum of all Price values
                        decimal totalSum = kvp.Value.Sum(tuple => tuple.Item3);


                        // Create the summaryTable with 2 columns
                        Table summaryTable = new Table(new float[] { 100, 75 });

                        // Add header row
                        summaryTable.AddHeaderCell("До сплати");
                        summaryTable.AddHeaderCell(totalSum.ToString());

                        summaryTable.SetHorizontalAlignment(HorizontalAlignment.RIGHT);

                        // Add the summaryTable to the document
                        document.Add(summaryTable);

                        // Add additional text with strong formatting
                        Paragraph strongText = new Paragraph("Прохання сплатити протягом 10 днів, але не пізніше 20 числа!");
                        document.Add(strongText);
                        pdfFilePaths.Add(pdfFilePath);
                    }

                }
            }
        }

        private void CreateZipFile()
        {
            string zipFileName = $"receipts_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.zip";
            string zipFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", zipFileName);

            // Create a zip file
            using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                // Add each PDF file to the zip archive
                foreach (var pdfPath in pdfFilePaths)
                {
                    // Get the file name from the path
                    string pdfFileName = Path.GetFileName(pdfPath);

                    // Create an entry in the zip archive
                    var entry = zipArchive.CreateEntry(pdfFileName);

                    // Open the entry for writing
                    using (var entryStream = entry.Open())
                    using (var pdfStream = File.OpenRead(pdfPath))
                    {
                        // Copy the PDF file to the zip archive
                        pdfStream.CopyTo(entryStream);
                    }
                }
            }

            // Delete individual PDF files after zipping
            foreach (var pdfPath in pdfFilePaths)
            {
                File.Delete(pdfPath);
            }

            // Clear the list of PDF file paths
            pdfFilePaths.Clear();
        }


    }
}

