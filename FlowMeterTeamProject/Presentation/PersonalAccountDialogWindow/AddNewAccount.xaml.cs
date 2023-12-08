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
        public List<string> MyItems { get; set; } = new List<string>();
        public AddNewAccount()
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
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
