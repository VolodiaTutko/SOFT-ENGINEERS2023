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

namespace Presentation.PersonalAccountDialogWindow
{
    /// <summary>
    /// Interaction logic for PropertiesAccounts.xaml
    /// </summary>
    public partial class PropertiesAccounts : Window
    {
        public PropertiesAccounts()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //EditData2 editData2 = new EditData2();
            //editData2.ShowDialog();
        }
    }
}
