using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Data.SqlClient;
using System.Net;
using FlowMeterTeamProject.Data;

namespace FlowMeterTeamProject.Pages
{
    /// <summary>
    /// Interaction logic for Counters.xaml
    /// </summary>
    public partial class Counters : Page
    {
        public Counters()
        {
            InitializeComponent();
            currentDateTextBlock.Text = DateTime.Now.ToString("dd-MM-yyyy");

            FillDataGrid();
        }

        public void FillDataGrid()
        {
            using (var context = new AppDbContext())
            {
                List<Counter> counters = context.counters.ToList();

                DataTable dt = new DataTable("Counter");
                dt.Columns.Add("Number", typeof(int));
                dt.Columns.Add("CountersId", typeof(int));
                dt.Columns.Add("PreviousIndicator", typeof(decimal));
                dt.Columns.Add("CurrentIndicator", typeof(decimal));
                dt.Columns.Add("Account", typeof(string));
                dt.Columns.Add("TypeOfAccount", typeof(string));
                dt.Columns.Add("Date", typeof(DateTime));

                for (int i = 0; i < counters.Count; i++)
                {
                    dt.Rows.Add(
                        i + 1,
                        counters[i].CountersId,
                        counters[i].PreviousIndicator,
                        counters[i].CurrentIndicator,
                        counters[i].Account,
                        counters[i].TypeOfAccount,
                        counters[i].Date
                    );
                }

                dt.Columns["Number"].SetOrdinal(0);

                dataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void b1_Click(object sender, EventArgs e)
        {

        }
    }
}