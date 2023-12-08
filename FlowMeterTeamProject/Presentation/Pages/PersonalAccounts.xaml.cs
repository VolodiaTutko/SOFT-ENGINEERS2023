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
using FlowMeterTeamProject.Presentation.PersonalAccountDialogWindow;
using FlowMeterTeamProject.Presentation;

namespace Presentation.Pages
{
    /// <summary>
    /// Interaction logic for PersonalAccounts.xaml
    /// </summary>
    public partial class PersonalAccounts : Page, IDataGridUpdater
    {       
        public PersonalAccounts()
        {
            InitializeComponent();

            FillDataGrid();

            dataGrid.MouseRightButtonDown += DataGrid_MouseRightButtonDown;
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
                List<Consumer> personalAccounts = context.consumers.ToList();

                DataTable dt = new DataTable("PesronalAccount");
                dt.Columns.Add("№", typeof(int));
                dt.Columns.Add("PersonalAccount", typeof(string));
                dt.Columns.Add("Owner", typeof(string));
                dt.Columns.Add("House", typeof(string));
                dt.Columns.Add("Flat", typeof(decimal));
                dt.Columns.Add("Area", typeof(decimal));
                dt.Columns.Add("NumberOfPerson", typeof(decimal));


                for (int i = 0; i < personalAccounts.Count; i++)
                {

                    dt.Rows.Add(
                        i + 1,
                        personalAccounts[i].PersonalAccount,
                        personalAccounts[i].ConsumerOwner,
                        context.houses
                        .Where(h => h.HouseId == personalAccounts[i].HouseId)
                        .Select(h => h.HouseAddress)
                        .FirstOrDefault(),
                        personalAccounts[i].Flat,
                        personalAccounts[i].HeatingArea,
                        personalAccounts[i].NumberOfPersons
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
            var addNewAccount = new AddNewAccount(this);
            addNewAccount.ShowDialog();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                // Показати вікно PropertiesAcconts
                var propertiesWindow = new PropertiesAccounts();
                propertiesWindow.ShowDialog();
            }
        }
    }
}
