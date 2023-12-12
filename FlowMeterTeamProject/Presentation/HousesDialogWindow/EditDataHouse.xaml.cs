namespace Presentation.HousesDialogWindow
{
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

    /// <summary>
    /// Interaction logic for EditDataHouse.xaml
    /// </summary>
    public partial class EditDataHouse : Window
    {

        public int houseID;

        private IDataGridUpdater _dataGridUpdater;
        public EditDataHouse(int HouseID, IDataGridUpdater dataGridUpdater)
        {

            _dataGridUpdater = dataGridUpdater;
            this.InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            houseID = HouseID;

            try
            {
                using (var context = new AppDbContext())
                {
                    var houseToEdit = context.houses.FirstOrDefault(c => c.HouseId == houseID);
                    int? houseFlat;

                    if (houseToEdit != null && houseToEdit.NumberOfFlat != null)
                    {
                        houseFlat = houseToEdit.NumberOfFlat;
                        NumberOfFlatText.Text = houseFlat?.ToString();
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show($"Не вдалося знайти запис для Будинку: {houseToEdit.HouseAddress}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                  }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні  даних про будинок: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {

                    var House = context.houses.FirstOrDefault(h => h.HouseId == houseID);
                    if (House != null)
                    {
                       
                        int NumberOfFlat = int.TryParse(NumberOfFlatText.Text, out int result) ? result : 0;
                        
                        House.NumberOfFlat = NumberOfFlat;
                      
                        context.SaveChanges();
                        _dataGridUpdater?.UpdateDataGrid();
                        this.Close();
                        MessageBox.Show($"Внесено зміни  до  будинку : {House.HouseAddress}");
                    }
                    else
                    {
                        MessageBox.Show($"Не вдалося зберегти  запис для будинку: {House.HouseAddress}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
