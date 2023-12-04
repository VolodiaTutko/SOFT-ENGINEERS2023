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
        private Brush activeButtonColor = new SolidColorBrush(Color.FromRgb(176, 176, 176));
        public MainContent()
        {
            InitializeComponent();

            Btn1.Background = activeButtonColor;
            Main.NavigationService.Navigate(new FlowMeterTeamProject.Pages.Houses());
        }

        private void ResetButtonColors()
        {
            Btn1.Background = Brushes.Transparent;
            Btn2.Background = Brushes.Transparent;
            Btn3.Background = Brushes.Transparent;
            Btn4.Background = Brushes.Transparent;
            Btn5.Background = Brushes.Transparent;
            Btn6.Background = Brushes.Transparent;
        }


        private void BtnClick1(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.Houses();
            ResetButtonColors();
            Btn1.Background = activeButtonColor;
        }
        private void BtnClick2(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.PersonalAccounts();
            ResetButtonColors();
            Btn2.Background = activeButtonColor;
        }
        private void BtnClick3(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.Counters();
            ResetButtonColors();
            Btn3.Background = activeButtonColor;
        }
        private void BtnClick4(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.Services();
            ResetButtonColors();
            Btn4.Background = activeButtonColor;
        }
        private void BtnClick5(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.Payments();
            ResetButtonColors();
            Btn5.Background = activeButtonColor;
        }
        private void BtnClick6(object sender, RoutedEventArgs e)
        {
            Main.Content = new FlowMeterTeamProject.Pages.Receipts();
            ResetButtonColors();    
            Btn6.Background = activeButtonColor;
        }
    }
}
