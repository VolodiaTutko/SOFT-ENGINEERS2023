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

namespace FlowMeterTeamProject.Presentation.DialogWindows
{
    public partial class NewServiceDialog : Window
    {
        public List<string> MyItems { get; set; } = new List<string>();
        private Dictionary<string, string> accountsName;
        private Dictionary<string, string> typesName;
        private Dictionary<string, string> units;
        public NewServiceDialog()
        {
            this.InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;

            using (var context = new AppDbContext())
            {
                foreach (var item in context.houses.ToList())
                {
                    MyItems.Add(item.HouseAddress.ToString());
                }

                MyItems.Insert(0, "Оберіть будинок");
                HouseComboBox.ItemsSource = MyItems;
                HouseComboBox.SelectedIndex = 0;


            };

            typesName = new Dictionary<string, string>
            {
                {"Default", "Оберіть послугу"},
                {"HotWater", "Гаряча вода"},
                {"ColdWater", "Холодна вода"},
                {"Heating", "Опалення"},
                {"Electricity", "Електрика"},
                {"PublicService", "Комунальні послуги"}
            };

            TypeComboBox.ItemsSource = typesName.Values;
            TypeComboBox.SelectedIndex = 0;

            units = new Dictionary<string, string>
            {
                {"Cubic meters", "Метри кубічні"},
                {"Square meters", "Метри квадратні"},
                {"Kilowatt", "Кіловат"}
            };


            UnitComboBox.ItemsSource = units.Values;
            UnitComboBox.SelectedIndex = 0;
        }
        private IDataGridUpdater _dataGridUpdater;

        public NewServiceDialog(IDataGridUpdater dataGridUpdater)
        {
            this.InitializeComponent();
            _dataGridUpdater = dataGridUpdater;

            using (var context = new AppDbContext())
            {
                foreach (var item in context.houses.ToList())
                {
                    MyItems.Add(item.HouseAddress.ToString());
                }

                MyItems.Insert(0, "Оберіть будинок");
                HouseComboBox.ItemsSource = MyItems;
                HouseComboBox.SelectedIndex = 0;


            };

            typesName = new Dictionary<string, string>
            {
                {"Default", "Оберіть послугу"},
                {"HotWater", "Гаряча вода"},
                {"ColdWater", "Холодна вода"},
                {"Heating", "Опалення"},
                {"Electricity", "Електрика"},
                {"PublicService", "Комунальні послуги"}
            };

            TypeComboBox.ItemsSource = typesName.Values;
            TypeComboBox.SelectedIndex = 0;

            units = new Dictionary<string, string>
            {
                {"Cubic meters", "Метри кубічні"},
                {"Square meters", "Метри квадратні"},
                {"Kilowatt", "Кіловат"}
            };

            UnitComboBox.ItemsSource = units.Values;
            UnitComboBox.SelectedIndex = 0;
            _dataGridUpdater = dataGridUpdater;
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string houseAddress = HouseComboBox.SelectedItem.ToString();
                string typeOfAccountUkrainian = TypeComboBox.SelectedItem.ToString();
                string unitUkrainian = UnitComboBox.SelectedItem.ToString();
                string priceText = PriceTextBox.Text;

                string typeOfAccount = GetTypeField(typeOfAccountUkrainian);
                string unit = GetUnitsField(unitUkrainian);
                int? price = int.TryParse(priceText, out int priceValue) ? priceValue : (int?)null;

                int? houseId;
                using (var context = new AppDbContext())
                {
                    houseId = context.houses
                        .Where(h => h.HouseAddress == houseAddress)
                        .Select(h => h.HouseId)
                        .FirstOrDefault();
                }

                using (var context = new AppDbContext())
                {
                    Service existingService = context.services
                        .Where(s => s.TypeOfAccount == typeOfAccount && s.HouseId == houseId)
                        .FirstOrDefault();

                    if (existingService != null)
                    {
                        existingService.Price = price;
                        context.SaveChanges();
                        MessageBox.Show($"Ціни для {typeOfAccount} було оновлено.");
                    }
                    else
                    {
                        Service newService = new Service
                        {
                            HouseId = houseId,
                            TypeOfAccount = typeOfAccount,
                            Price = price
                        };

                        context.services.Add(newService);
                        context.SaveChanges();
                        MessageBox.Show($"Додано нову послугу: {newService.TypeOfAccount}");
                    }
                }

                this._dataGridUpdater?.UpdateDataGrid();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private string GetAccountField(string ukrainianName)
        {
            return accountsName.FirstOrDefault(x => x.Value == ukrainianName).Key;
        }
        private string GetTypeField(string ukrainianName)
        {
            return typesName.FirstOrDefault(x => x.Value == ukrainianName).Key;
        }
        private string GetUnitsField(string ukrainianName)
        {
            return units.FirstOrDefault(x => x.Value == ukrainianName).Key;
        }
    }
}
