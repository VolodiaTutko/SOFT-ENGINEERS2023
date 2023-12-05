using FlowMeterTeamProject.Data;
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

namespace FlowMeterTeamProject.Pages
{
    /// <summary>
    /// Interaction logic for Services.xaml
    /// </summary>
    public partial class Services : Page
    {
        public Services()
        {
            InitializeComponent();
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            using (var context = new FlowMeterTeamProject.Data.AppDbContext())
            {
                List<Service> services = context.services.ToList();

                DataTable dt = new DataTable("Service");
                dt.Columns.Add("Number", typeof(int));
                dt.Columns.Add("ServiceId", typeof(int));
                dt.Columns.Add("HouseId", typeof(int));
                dt.Columns.Add("TypeOfAccount", typeof(string));
                dt.Columns.Add("Price", typeof(int));

                for (int i = 0; i < services.Count; i++)
                {
                    dt.Rows.Add(i + 1, services[i].ServiceId, services[i].HouseId, services[i].TypeOfAccount, services[i].Price);
                }

                dt.Columns["Number"].SetOrdinal(0);

                dataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void b1_Click(object sender, EventArgs e)
        {
            // Your button click logic here
        }
    }
}
