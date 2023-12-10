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

namespace FlowMeterTeamProject.Presentation.Features.Counters
{
     using FlowMeterTeamProject.BLL.Features.Counters;
    using Microsoft.Extensions.Primitives;

    /// <summary>
    /// Interaction logic for AddNewCounter.xaml
    /// </summary>
    public partial class AddNewCounter : Window
    {

        private CounterCreation creation;
        public List<string> consumers { get; set; } = new List<string>();
        public AddNewCounter()
        {
            InitializeComponent();
            this.creation = new CounterCreation();
            consumers = creation.GetAvailableConsumers();
            ConsumerComboBox.ItemsSource = consumers;
        }

        private IDataGridUpdater _dataGridUpdater;

        public AddNewCounter(IDataGridUpdater dataGridUpdater)
        {
            InitializeComponent();
            this.creation = new CounterCreation();
            consumers = creation.GetAvailableConsumers();
            ConsumerComboBox.ItemsSource = consumers;
            _dataGridUpdater = dataGridUpdater;
        }

        private void ConsumerComboBox_Change(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedValue = (string)comboBox.SelectedItem;

            creation.SetConsumer(selectedValue);
            ServiceComboBox.ItemsSource = creation.GetConsumerServices();
        }

        private void ServiceComboBox_Change(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedValue = (string)comboBox.SelectedItem;

            creation.SetTypeOfAccount(selectedValue);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;

            if (datePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = datePicker.SelectedDate.Value;
                creation.SetDate(selectedDate);
            }
        }

        private void InitialIndicatorTextBox_Changed(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text;
            Boolean parsed = decimal.TryParse(newText, out decimal decimalValueTryParsed);
            if (parsed)
            {
                creation.SetIndicator(decimalValueTryParsed);
            }
            else
            {
                creation.SetIndicator(0);
                InitialIndicatorTextBox.Text = "";
                MessageBox.Show($"Enter valid indicator. Only digits are allowed");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (creation.IsReadyToCreate())
            {
                try {
                    creation.Create();
                    _dataGridUpdater?.UpdateDataGrid();
                    this.Close();
                    MessageBox.Show($"New indicator value saved successfully");
                }
                catch
                {
                    MessageBox.Show($"Unknown error occured");
                }
            }
            else {
                MessageBox.Show($"All fields are required");
            }
        }
    }
}
