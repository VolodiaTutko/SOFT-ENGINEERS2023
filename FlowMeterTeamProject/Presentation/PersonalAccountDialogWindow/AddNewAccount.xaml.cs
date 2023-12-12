using DAL.Data;
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

namespace FlowMeterTeamProject.Presentation.PersonalAccountDialogWindow
{
    /// <summary>
    /// Interaction logic for AddNewAccount.xaml
    /// </summary>
    public partial class AddNewAccount : Window
    {
        private IDataGridUpdater _dataGridUpdater;

        public List<string> MyItems { get; set; } = new List<string>();
        public AddNewAccount(IDataGridUpdater dataGridUpdater)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            using (var context = new AppDbContext())
            {
                 foreach(var item in context.houses.ToList())
                {                   
                    MyItems.Add(item.HouseAddress.ToString());
                }

                MyItems.Insert(0, "Оберіть будинок");
                HouseComboBox.ItemsSource = MyItems;
                HouseComboBox.SelectedIndex = 0;


            };
            _dataGridUpdater = dataGridUpdater;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveNewAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var selectedItem = HouseComboBox.SelectedItem;
                    string selectedValue = selectedItem.ToString();

                    int houseid = context.houses
                        .Where(h => h.HouseAddress == selectedValue)
                        .Select(h => h.HouseId)
                        .FirstOrDefault();

                    string owner = OwnerTextBox.Text;
                    int flat = int.Parse(FlatTextBox.Text);
                    int heatingArea = int.Parse(HeatingAreaTextBox.Text);
                    string personalAccount = PersonalAccountTextBox.Text;
                    int numberOfPersons = int.Parse(NumberOfPersonsTextBox.Text);

                    // Check if the personal account already exists
                    if (context.consumers.Any(c => c.HouseId == houseid && c.Flat == flat || c.PersonalAccount == personalAccount))
                    {
                        MessageBox.Show($"Такий запис вже існує.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Do not proceed further if the personal account already exists
                    }

                    Consumer newPersonalAccount = new Consumer
                    {
                        HouseId = houseid,
                        ConsumerOwner = owner,
                        Flat = flat,
                        HeatingArea = heatingArea,
                        PersonalAccount = personalAccount,
                        NumberOfPersons = numberOfPersons
                    };

                    context.consumers.Add(newPersonalAccount);
                    Account newAccount = new Account
                    {
                        PersonalAccount = personalAccount
                    };
                    context.accounts.Add(newAccount);
                    context.SaveChanges();
                    _dataGridUpdater?.UpdateDataGrid();
                    this.Close();
                    MessageBox.Show($"Додано новий особовий рахунок: {newPersonalAccount.PersonalAccount}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {

        }
    }
}
