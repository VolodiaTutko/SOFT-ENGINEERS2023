using FlowMeterTeamProject.Presentation;
using FlowMeterTeamProject.Presentation.PersonalAccountDialogWindow;
using Presentation.Pages;
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
        public string personalacount;
        public PropertiesAccounts(string PersonalAccount)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            personalacount = PersonalAccount;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var editDataAccounts = new EditDataAccounts();
            editDataAccounts.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void SubAccounts(object sender, RoutedEventArgs e)
        {
            var subAccounts = new SubAccounts(personalacount);
            subAccounts.LabelPersonalAccount.Content = personalacount;
            subAccounts.ShowDialog();

        }
    }
}
