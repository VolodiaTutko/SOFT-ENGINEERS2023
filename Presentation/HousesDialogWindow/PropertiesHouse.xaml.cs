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

namespace Presentation.HousesDialogWindow
{
    /// <summary>
    /// Interaction logic for PropertiesHouse.xaml
    /// </summary>
    public partial class PropertiesHouse : Window
    {
        public PropertiesHouse()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //EditData editData = new EditData();
            //editData.Show();
        }
    }
}
