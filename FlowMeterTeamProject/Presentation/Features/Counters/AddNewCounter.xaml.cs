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



        //
        //
        //

        //
        // todo t3: add new record window
        // show last value using new method of counter service
        // validate values using another new method of counter service ON data input

        private CounterRecordAdding creation;
        private List<string> consumers { get; set; }
        private List<string> houses { get; set; }

        private IDataGridUpdater _dataGridUpdater;

        public AddNewCounter(IDataGridUpdater dataGridUpdater, CounterCreationType addingType, string operableServicesLabel, string unoperableServicesLabel)
        {
            InitializeComponent();
            this.creation = CounterCreationInjection.GetInstanceOf(addingType);
            consumers = creation.GetAvailableConsumers();
            houses = creation.GetAvailableHouses();
            ConsumerComboBox.ItemsSource = consumers;
            HouseComboBox.ItemsSource = houses;
            NotOperableServicesLabelText.Text = unoperableServicesLabel;
            OperableServicesLabel.Content = operableServicesLabel;
            _dataGridUpdater = dataGridUpdater;
        }

        private void ConsumerComboBox_Change(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedValue = (string)comboBox.SelectedItem;

            creation.SetConsumer(selectedValue);
            ServiceComboBox.ItemsSource = creation.GetOperableServices();
            NotOperableServicesLabelText.Visibility = Visibility.Visible;
            NotOperableServicesListText.Text = string.Join(", ", creation.GetUnoperableServices());
        }

        private void HouseComboBox_Change(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedValue = (string)comboBox.SelectedItem;

            creation.SetHouse(selectedValue);
            ConsumerComboBox.ItemsSource = creation.GetAvailableConsumers();
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
            if (creation.IsReadyToAdd())
            {
                try {
                    creation.Add();
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
