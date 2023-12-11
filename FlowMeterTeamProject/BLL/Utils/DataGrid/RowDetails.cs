using DAL.Data;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterTeamProject.BLL.Utils.DataGrid
{
    public class RowDetails
    {
        private readonly List<Dictionary<string, string>> rowsData;

        public RowDetails(List<Dictionary<string, string>> rowsData)
        {
            //this.InitializeComponent();
            this.rowsData = rowsData;
            int rowCount = 0;
            string logMessage = " ";
            var finalsublist = new Dictionary<string, List<(string ServiceName, string AccountNumber, decimal Price)>>();
            using (var dbContext = new AppDbContext())
            {
                foreach (var rowData in rowsData)
                {
                    foreach (var pair in rowData)
                    {
                        //this.detailsTextBlock.Text += $"{pair.Key}: {pair.Value}\n";
                        if (pair.Key == "Особовий рахунок")
                        {
                            string houseAddressValue;
                            _ = rowData.TryGetValue("Адреса будинку", out houseAddressValue);
                            finalsublist = ReceiptsLogic.GetNonZeroServices(pair.Value, houseAddressValue);
                            Dictionary<string, List<object>> newPair = new Dictionary<string, List<object>>();
                        }

                        logMessage += $"{pair.Key}: {pair.Value}\n";
                    }

                    foreach (var kvp in finalsublist)
                    {
                        foreach (var tuple in kvp.Value)
                        {
                            logMessage += $"\t  ServiceName: {tuple.ServiceName}, AccountNumber: {tuple.AccountNumber}, Price: {tuple.Price}\n";
                        }
                    }

                    // this.detailsTextBlock.Text += "------------------------\n";
                    rowCount++;
                }

                string logFilePath = "testlogicReceipt.txt";
                // string logMessage =  $"  rowsData: {rowsData}\n";
                File.WriteAllText(logFilePath, $"{DateTime.Now}: {logMessage}\n");
            }
        }
    }
}

