using DAL.Data;
using FlowMeterTeamProject.BLL.Utils.DataGrid;
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
using System;
using System.Collections.Generic;
using System.IO;
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
using DAL.Data;
using FlowMeterTeamProject.BLL.Utils.DataGrid;


namespace FlowMeterTeamProject.Presentation.PersonalAccountDialogWindow
{
    /// <summary>
    /// Interaction logic for PropertiesAccountsForMany.xaml
    /// </summary>
    public partial class PropertiesAccountsForMany : Window
    {

        List<Dictionary<string, string>> SelectedRowsData;

        private IDataGridUpdater _dataGridUpdater;
        public PropertiesAccountsForMany(List<Dictionary<string, string>> selectedRowsData, IDataGridUpdater dataGridUpdater)
        {
            this.InitializeComponent();

            SelectedRowsData = selectedRowsData;

            _dataGridUpdater = dataGridUpdater;
        }

        private void DeleteAll(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    foreach (var rowData in SelectedRowsData)
                    {
                        string personalAccount = rowData["Особовий рахунок"];

                      
                        var consumerToDelete = context.consumers.FirstOrDefault(c => c.PersonalAccount == personalAccount);

                      
                        var accountToDelete = context.accounts.FirstOrDefault(a => a.PersonalAccount == personalAccount);

                        if (consumerToDelete != null)
                        {
                         
                            context.consumers.Remove(consumerToDelete);
                        }

                        if (accountToDelete != null)
                        {
                           
                            context.accounts.Remove(accountToDelete);
                        }
                    }

                    
                    context.SaveChanges();
                    this._dataGridUpdater?.UpdateDataGrid();
                    this.Close();
                    MessageBox.Show("Видалено всі обрані записи.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateReceipts(object sender, RoutedEventArgs e)
        {
            try
            {
                RowDetails receiptsWindow = new RowDetails(SelectedRowsData);
                this.Close();
                MessageBox.Show($"Квитанції збережено та завантажено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при формуванні квитанції: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
