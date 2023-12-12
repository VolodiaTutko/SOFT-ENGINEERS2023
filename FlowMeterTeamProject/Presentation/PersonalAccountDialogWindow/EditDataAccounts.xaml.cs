using DAL.Data;
using FlowMeterTeamProject.Presentation;
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
    /// Interaction logic for EditDataAccounts.xaml
    /// </summary>
    public partial class EditDataAccounts : Window
    {
        private IDataGridUpdater _dataGridUpdater;

        public string personalAccount;

        private PersonalAccounts _firstWindow;
        public EditDataAccounts(string PersonalAccount, IDataGridUpdater dataGridUpdater)
        {

            _dataGridUpdater = dataGridUpdater;


            personalAccount = PersonalAccount;
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            
            try
            {
                using (var context = new AppDbContext())
                {
                    var consumer = context.consumers.FirstOrDefault(h => h.PersonalAccount == PersonalAccount);

                    if (consumer != null)
                    {
                        
                        string ConsumerOwner = consumer.ConsumerOwner?.ToString() ?? "";
                        int NumberOfPersons;
                       
                       if(consumer.NumberOfPersons != null)
                        {
                            NumberOfPersons = consumer.NumberOfPersons;
                        }
                        else
                        {
                            NumberOfPersons = 0;
                        }


                        OwnerText.Text = ConsumerOwner;
                        NumberText.Text = NumberOfPersons.ToString();
                    }
                    else
                    {
                        MessageBox.Show($"Не вдалося знайти запис для особового рахунку: {PersonalAccount}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    
                    var consumer = context.consumers.FirstOrDefault(h => h.PersonalAccount == personalAccount);
                    if (consumer != null)
                    {
                        string ConsumerOwner = OwnerText.Text;
                        int NumberOfPersons = int.TryParse(NumberText.Text, out int result) ? result : 0;

                        //--------------------------------// 
                        consumer.ConsumerOwner = ConsumerOwner;
                        consumer.NumberOfPersons = NumberOfPersons;
                        context.SaveChanges();
                        _dataGridUpdater?.UpdateDataGrid();
                        this.Close();
                        MessageBox.Show($"Внесено зміни  до  рахунку : {consumer.PersonalAccount}");
                    }
                    else
                    {
                        MessageBox.Show($"Не вдалося зберегти  запис для особового рахунку: {consumer.PersonalAccount}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
