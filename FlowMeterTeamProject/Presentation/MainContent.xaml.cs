namespace Presentation
{
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

    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    public partial class MainContent : UserControl
    {
        private Brush activeButtonColor = new SolidColorBrush(Color.FromRgb(176, 176, 176));

        public MainContent()
        {
            this.InitializeComponent();

            this.Btn1.Background = this.activeButtonColor;
            this.Main.NavigationService.Navigate(new Pages.Houses());
        }

        private void ResetButtonColors()
        {
            this.Btn1.Background = Brushes.Transparent;
            this.Btn2.Background = Brushes.Transparent;
            this.Btn3.Background = Brushes.Transparent;
            this.Btn4.Background = Brushes.Transparent;
            this.Btn5.Background = Brushes.Transparent;
            this.Btn6.Background = Brushes.Transparent;
        }

        private void BtnClick1(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.Houses();
            this.ResetButtonColors();
            this.Btn1.Background = this.activeButtonColor;
        }

        private void BtnClick2(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.PersonalAccounts();
            this.ResetButtonColors();
            this.Btn2.Background = this.activeButtonColor;
        }

        private void BtnClick3(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.Counters();
            this.ResetButtonColors();
            this.Btn3.Background = this.activeButtonColor;
        }

        private void BtnClick4(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.Services();
            this.ResetButtonColors();
            this.Btn4.Background = this.activeButtonColor;
        }

        private void BtnClick5(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.Payments();
            this.ResetButtonColors();
            this.Btn5.Background = this.activeButtonColor;
        }

        private void BtnClick6(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.Receipts();
            this.ResetButtonColors();
            this.Btn6.Background = this.activeButtonColor;
        }
    }
}
