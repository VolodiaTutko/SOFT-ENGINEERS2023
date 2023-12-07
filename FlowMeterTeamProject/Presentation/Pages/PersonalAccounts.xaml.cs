using DAL.Data;
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
                List<Account> accounts = context.accounts.ToList();

                DataTable dt = new DataTable("Account");
                dt.Columns.Add("№", typeof(int));
                dt.Columns.Add("PersonalAccount", typeof(string));
                dt.Columns.Add("HotWater", typeof(decimal));
                dt.Columns.Add("ColdWater", typeof(decimal));
                dt.Columns.Add("Heating", typeof(decimal));
                dt.Columns.Add("Electricity", typeof(decimal));
                dt.Columns.Add("PublicService", typeof(decimal));

                for (int i = 0; i < accounts.Count; i++)
                {
                    dt.Rows.Add(
                        i + 1,
                        accounts[i].PersonalAccount,
                        accounts[i].HotWater,
                        accounts[i].ColdWater,
                        accounts[i].Heating,
                        accounts[i].Electricity,
                        accounts[i].PublicService
                    );
                }

                dt.Columns["№"].SetOrdinal(0);

                dataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void DataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)(dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem));

            if (row != null)
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

                RowDetails dialog = new RowDetails(rowData);
                dialog.Owner = Window.GetWindow(this);
                dialog.ShowDialog();
            }
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

    }
}
