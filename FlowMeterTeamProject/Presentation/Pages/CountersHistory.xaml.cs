using FlowMeterTeamProject.BLL.Features.Counters;
using FlowMeterTeamProject.Presentation.Features.Counters;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowMeterTeamProject.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for CountersHistory.xaml
    /// </summary>
    public partial class CountersHistory : Page, IDataGridUpdater
    {
        private CountersInfo countersInfo;
        public CountersHistory()
        {
            InitializeComponent();
            this.countersInfo = new CountersInfo();
            currentDateTextBlock.Text = DateTime.Now.ToString("dd-MM-yyyy");

            FillDataGrid();
        }
        public event EventHandler DataGridUpdated;

        public void UpdateDataGrid()
        {
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            List<CounterRecord> counters = countersInfo.GetCounterRecords();

            DataTable dt = new DataTable("Counter");
            dt.Columns.Add("Number", typeof(int));
            dt.Columns.Add("Account", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Current Value", typeof(decimal));
            dt.Columns.Add("Value as of date", typeof(string));

            for (int i = 0; i < counters.Count; i++)
            {
                dt.Rows.Add(
                    i + 1,
                    counters[i].Account,
                    counters[i].TypeOfAccount,
                    counters[i].CurrentValue,
                    counters[i].LastModified.ToString("dd/MM/yyyy")
                );
            }

            dt.Columns["Number"].SetOrdinal(0);

            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            AddNewCounter newCounterDialog = new AddNewCounter(this);
            newCounterDialog.Show();
        }

        private void b1_Click(object sender, EventArgs e)
        {

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
