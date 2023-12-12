namespace Presentation.HousesDialogWindow
{
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
    using DAL.Data;
    using FlowMeterTeamProject.Presentation;
    using Presentation.Pages;

    /// <summary>
    /// Interaction logic for AddNewHouse.xaml
    /// </summary>
    public partial class AddNewHouse : Window
    {
        public AddNewHouse()
        {
            this.InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;

        }

        private IDataGridUpdater _dataGridUpdater;

        public AddNewHouse(IDataGridUpdater dataGridUpdater)
        {
            this.InitializeComponent();
            _dataGridUpdater = dataGridUpdater;
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveNewHouse(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    string address = this.AddressTextBox.Text;
                    int numberOfFlats = int.Parse(this.NumberOfFlatsTextBox.Text);
                    int heatingArea = int.Parse(this.HeatingAreaTextBox.Text);
                    int numberOfResidents = int.Parse(this.NumberOfResidentsTextBox.Text);
                    House newHouse = new House
                    {
                        HouseAddress = address,
                        NumberOfFlat = numberOfFlats,
                        HeatingAreaOfHouse = heatingArea,
                        NumberOfResidents = numberOfResidents,
                    };
                    context.houses.Add(newHouse);
                    context.SaveChanges();
                    this._dataGridUpdater?.UpdateDataGrid();
                    this.Close();
                    MessageBox.Show($"Додано новий будинок: {newHouse.HouseAddress}");
                }
            }
            catch (Exception ex)
            {
                // Опціонально: обробте помилку (виведення повідомлення або логування)
                MessageBox.Show($"Помилка при збереженні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
