using DAL.Data;
using FlowMeterTeamProject.BLL.Utils.DataGrid;
using FlowMeterTeamProject.Presentation;
using FlowMeterTeamProject.Presentation.PersonalAccountDialogWindow;
using Presentation.Pages;
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


namespace Presentation.PersonalAccountDialogWindow
{
    /// <summary>
    /// Interaction logic for PropertiesAccounts.xaml
    /// </summary>
    public partial class PropertiesAccounts : Window
    {
        public string personalacount;

       
        List<Dictionary<string, string>> SelectedRowsData;


        private IDataGridUpdater _dataGridUpdater;
        public PropertiesAccounts(string PersonalAccount, List<Dictionary<string, string>> selectedRowsData, IDataGridUpdater dataGridUpdater)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            personalacount = PersonalAccount;
            _dataGridUpdater = dataGridUpdater;

            SelectedRowsData = selectedRowsData;

           }

       

               private void EditData(object sender, RoutedEventArgs e)
        {
            
            var editDataAccounts = new EditDataAccounts(personalacount, _dataGridUpdater);
            editDataAccounts.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void SubAccounts(object sender, RoutedEventArgs e)
        {
            var subAccounts = new SubAccounts(personalacount);
            subAccounts.LabelPersonalAccount.Content = personalacount;
            subAccounts.ShowDialog();

        }

        private void CreateReceipts(object sender, RoutedEventArgs e)
        {
            try
            {
                RowDetails receiptsWindow = new RowDetails(SelectedRowsData);
                this.Close();
                MessageBox.Show($"Квитанцію збережено та завантажено в папку {Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads"}", "Saved Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при формуванні квитанції: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {                  
                    var consumerToDelete = context.consumers.FirstOrDefault(c => c.PersonalAccount == personalacount);

                    
                    var accountToDelete = context.accounts.FirstOrDefault(a => a.PersonalAccount == personalacount);

                    if (consumerToDelete != null)
                    {
                        
                        context.consumers.Remove(consumerToDelete);
                    }

                    if (accountToDelete != null)
                    {
                        
                        context.accounts.Remove(accountToDelete);
                    }

                   
                    context.SaveChanges();
                    this._dataGridUpdater?.UpdateDataGrid();
                    this.Close();
                    MessageBox.Show($"Особовий рахунок: {personalacount} видалено!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
