using DAL.Data;
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
using DAL.Data.DataMock;
using Presentation.Pages;
using System.IO;
using BLL.Utils.DataGrid;
using FlowMeterTeamProject.BLL.Utils.DataGrid;
using System.Collections;

namespace Presentation
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //if (Mock.checkIfDbConsumersEmpty())
            //{
            //    Mock.FillRandomConsumersIntoDb(5);
            //}
            //PasswordHashing.HashPasswordAndAddUser("Jon", "12345", "user");
            //Mock.FillServicesIntoDb();
            //Mock.FillHauseIntoDb();
            //Mock.FillAccountIntoDb();
            //Mock.FillConsumersIntoDb();
            //Mock.FillCounterIntoDb();

        }

        private void SwitchToMain_Click(object sender, RoutedEventArgs e)
        {
            string login = textlogin.Text;
            string password = textpassword.Text;

            using (var dbContext = new AppDbContext())
            {
                var employee = dbContext.employees.FirstOrDefault(e => e.EmployeeLogin == login);
                if (employee != null && PasswordHashing.VerifyPassword(password,employee.EmployeePassword))
                {

                    Window newWindow = new Window
                    {
                        Content = new MainContent(),
                        Title = "FlowMeter"
                    };

                    Window.GetWindow(this).Close();
                    newWindow.Show();
                }
                else
                {
                    textlogin.Text = string.Empty;
                    textpassword.Text = string.Empty;
                }
            }

        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {

                DragMove();
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void TextBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            TextBlock backgroundText = (TextBlock)textBox.Template.FindName("BackgroundText", textBox);

            if (textBox.Text.Length == 0 && !textBox.IsFocused)
            {
                backgroundText.Visibility = Visibility.Visible;
            }
            else
            {
                backgroundText.Visibility = Visibility.Collapsed;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}