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
    using FlowMeterTeamProject.Presentation.Pages;

    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    public partial class MainContent : UserControl
    {
        private Brush activeButtonColor = new SolidColorBrush(Color.FromRgb(48, 90, 171));
        private Brush activeTextColor = Brushes.White;

        public MainContent()
        {
            this.InitializeComponent();

            this.Btn1.Background = this.activeButtonColor;
            this.Btn1.Foreground = this.activeTextColor;
            this.Btn1.FontWeight = FontWeights.Bold;

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

            this.Btn1.Foreground = Brushes.Black;
            this.Btn1.FontWeight = FontWeights.Normal;

            this.Btn2.Foreground = Brushes.Black;
            this.Btn2.FontWeight = FontWeights.Normal;

            this.Btn3.Foreground = Brushes.Black;
            this.Btn3.FontWeight = FontWeights.Normal;

            this.Btn4.Foreground = Brushes.Black;
            this.Btn4.FontWeight = FontWeights.Normal;

            this.Btn5.Foreground = Brushes.Black;
            this.Btn5.FontWeight = FontWeights.Normal;

            this.Btn6.Foreground = Brushes.Black;
            this.Btn6.FontWeight = FontWeights.Normal;
        }

        private void SetActiveButtonColors(Button btn)
        {
            btn.Background = this.activeButtonColor;
            btn.Foreground = this.activeTextColor;
            btn.FontWeight = FontWeights.Bold;
        }

        private void BtnClick1(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.Houses();
            this.ResetButtonColors();
            this.SetActiveButtonColors(this.Btn1);
        }

        private void BtnClick2(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.PersonalAccounts();
            this.ResetButtonColors();
            this.SetActiveButtonColors(this.Btn2);
        }

        private void BtnClick3(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.Counters();
            this.ResetButtonColors();
            this.SetActiveButtonColors(this.Btn3);
        }

        private void BtnClick4(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.Services();
            this.ResetButtonColors();
            this.SetActiveButtonColors(this.Btn4);
        }

        private void BtnClick5(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new CountersHistory();
            this.ResetButtonColors();
            this.SetActiveButtonColors(this.Btn5);
        }

        private void BtnClick6(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Pages.Receipts();
            this.ResetButtonColors();
            this.SetActiveButtonColors(this.Btn6);
        }
    }
}
