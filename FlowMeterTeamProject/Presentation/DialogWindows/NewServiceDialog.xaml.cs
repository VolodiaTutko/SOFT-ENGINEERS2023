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
        private IDataGridUpdater _dataGridUpdater;
        public List<string> MyItems { get; set; } = new List<string>();
        private Dictionary<string, string> accountsName;
        private Dictionary<string, string> typesName;
        private Dictionary<string, string> units;
        public NewServiceDialog()
        {
            InitializeComponent();
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
        private void CreateButton_Click(object sender, RoutedEventArgs e)
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

            Service newService = new Service
            {
                HouseId = houseId,
                TypeOfAccount = typeOfAccount,
                Price = price
            };

            using (var context = new AppDbContext())
            {
                context.services.Add(newService);
                context.SaveChanges();
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
