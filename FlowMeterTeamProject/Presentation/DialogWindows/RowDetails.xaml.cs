using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;
using DAL.Data;
using FlowMeterTeamProject.BLL.Utils.DataGrid;

namespace FlowMeterTeamProject.Presentation.DialogWindows
{
    /// <summary>
    /// Interaction logic for RowDetails.xaml
    /// </summary>
    public partial class RowDetails : Window
    {
        private readonly List<Dictionary<string, string>> rowsData;

        public RowDetails(List<Dictionary<string, string>> rowsData)
        {
            InitializeComponent();
            this.rowsData = rowsData;
            int rowCount = 0;
            string logMessage="";
            var finalsublist = new Dictionary<string, List<(string ServiceName, string AccountNumber, decimal Price)>>();
            using (var dbContext = new AppDbContext())
            {
                foreach (var rowData in rowsData)
                {
                    foreach (var pair in rowData)
                    {
                        detailsTextBlock.Text += $"{pair.Key}: {pair.Value}\n";
                        if(pair.Key== "Особовий рахунок")
                        {
                            string houseAddressValue;
                            rowData.TryGetValue("Адреса будинку", out houseAddressValue);
                            finalsublist = ReceiptsLogic.GetNonZeroServices(pair.Value, houseAddressValue);
                            Dictionary<string, List<object>> newPair = new Dictionary<string, List<object>>();
                            
                        }
                        logMessage += $"{pair.Key}: {pair.Value}\n";
                    }
                    foreach (var kvp in finalsublist)
                    {
                        foreach (var tuple in kvp.Value)
                        {
                            logMessage += ($"\t  ServiceName: {tuple.ServiceName}, AccountNumber: {tuple.AccountNumber}, Price: {tuple.Price}\n");
                        }
                    }
                    detailsTextBlock.Text += "------------------------\n";
                    rowCount++;
                }
                string logFilePath = "testlogicReceipt.txt";
                //string logMessage =  $"  rowsData: {rowsData}\n";
                File.WriteAllText(logFilePath, $"{DateTime.Now}: {logMessage}\n");
            }
        }
    }
}