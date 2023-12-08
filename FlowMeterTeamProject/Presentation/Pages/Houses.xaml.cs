using DAL.Data;
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
using Presentation.HousesDialogWindow;


namespace Presentation.Pages
{
    /// <summary>
    /// Interaction logic for Houses.xaml
    /// </summary>
    public partial class Houses : Page
    {
        public Houses()
        {
            InitializeComponent();
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            using (var context = new AppDbContext())
            {
                List<House> houses = context.houses.ToList();

                DataTable dt = new DataTable("House");
                dt.Columns.Add("Number", typeof(int));
                dt.Columns.Add("HouseAddress", typeof(string));
                dt.Columns.Add("HeatingAreaOfHouse", typeof(int));
                dt.Columns.Add("NumberOfFlat", typeof(int));
                dt.Columns.Add("NumberOfResidents", typeof(int));


                for (int i = 0; i < houses.Count; i++)
                {
                    dt.Rows.Add(i + 1, houses[i].HouseAddress, houses[i].HeatingAreaOfHouse, houses[i].NumberOfFlat, houses[i].NumberOfResidents);
                }

                dt.Columns["Number"].SetOrdinal(0);

                dataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void b1_Click(object sender, EventArgs e)
        {
            // Your button click logic here
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewHouse newDialog = new AddNewHouse();
            newDialog.Show();


        }
    }
}
