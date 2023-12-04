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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowMeterTeamProject
{
    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    public partial class MainContent : UserControl
    {
        public MainContent()
        {
            InitializeComponent();

            Main.NavigationService.Navigate(new FlowMeterTeamProject.Pages.Houses());
        }

        private void BtnClick1(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.Houses();
        }
        private void BtnClick2(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.PersonalAccounts();
        }
        private void BtnClick3(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.Counters();
        }
        private void BtnClick4(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.Services();
        }
        private void BtnClick5(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.Payments();
        }
        private void BtnClick6(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.Receipts();
        }
    }
}
