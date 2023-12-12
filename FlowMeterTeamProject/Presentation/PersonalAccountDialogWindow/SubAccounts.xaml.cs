using DAL.Data;
using Presentation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

namespace FlowMeterTeamProject.Presentation.PersonalAccountDialogWindow
{
    /// <summary>
    /// Interaction logic for SubAccounts.xaml
    /// </summary>
    public partial class SubAccounts : Window
    {

       
        public SubAccounts(string PersonalAccount)
        {
            InitializeComponent();
  
            this.ResizeMode = ResizeMode.NoResize;
            try
            {
                using (var context = new AppDbContext())
                {
                    var WithPersonalAccount = context.accounts.FirstOrDefault(h => h.PersonalAccount == PersonalAccount);
                    if (WithPersonalAccount != null)
                    {
                        string HotWater = WithPersonalAccount.HotWater?.ToString() ?? "0";
                        string ColdWater = WithPersonalAccount.ColdWater?.ToString() ?? "0";
                        string Heating = WithPersonalAccount.Heating?.ToString() ?? "0";
                        string Electricity = WithPersonalAccount.Electricity?.ToString() ?? "0";
                        string Gas = WithPersonalAccount.Gas?.ToString() ?? "0";
                        string PublicService = WithPersonalAccount.PublicService?.ToString() ?? "0";



                        HotWaterText.Text = HotWater;
                        ColdWaterText.Text = ColdWater;
                        HeatingText.Text = Heating;
                        ElectricityText.Text = Electricity;
                        GasText.Text = Gas;
                        PublicServiceText.Text = PublicService;
                    }
                    else
                    {
                        MessageBox.Show($"Не вдалося знайти запис для особового рахунку: {PersonalAccount}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                       

                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show($"Помилка при отриманні  даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
             
        }

        private void SaveSubAccounts(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {

                    string PA = LabelPersonalAccount.Content.ToString();

                    var WithPersonalAccount = context.accounts.FirstOrDefault(h => h.PersonalAccount == PA);
                    if (WithPersonalAccount != null)
                    {
                        string personalAccount = PA;
                        string hotWater = HotWaterText.Text;
                        string coldWater = ColdWaterText.Text;
                        string electricity = ElectricityText.Text;
                        string heating = HeatingText.Text;
                        string gas = GasText.Text;
                        string publicservice = PublicServiceText.Text;

                        //--------------------------------// 
                        WithPersonalAccount.PersonalAccount = personalAccount;
                        WithPersonalAccount.ColdWater = coldWater;
                        WithPersonalAccount.HotWater = hotWater;
                        WithPersonalAccount.Electricity = electricity;
                        WithPersonalAccount.Heating = heating;
                        WithPersonalAccount.Gas = gas;
                        WithPersonalAccount.PublicService = publicservice;
                        context.SaveChanges();
                        this.Close();
                        MessageBox.Show($"Прив'язано нові рахукни до : {WithPersonalAccount.PersonalAccount}");
                    }
                    else 
                    {
                        MessageBox.Show($"Не вдалося зберегти  запис для особового рахунку: {WithPersonalAccount.PersonalAccount}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
