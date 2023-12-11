namespace Presentation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
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
    using BLL.Utils.DataGrid;
    using DAL.Data;
    using DAL.Data.DataMock;
    using FlowMeterTeamProject.BLL.Utils.DataGrid;
    using Presentation.Pages;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // if (Mock.checkIfDbConsumersEmpty())
            // {
            //     Mock.FillRandomConsumersIntoDb(5);
            // }
            //PasswordHashing.HashPasswordAndAddUser("Jon", "12345", "user");
            //Mock.FillServicesIntoDb();
            //Mock.FillHauseIntoDb();
            //Mock.FillAccountIntoDb();
            //Mock.FillConsumersIntoDb();
            //Mock.FillCounterIntoDb();
        }

        private void SwitchToMain_Click(object sender, RoutedEventArgs e)
        {
            string login = this.textlogin.Text;
            string password = this.textpassword.Password;

            using (var dbContext = new AppDbContext())
            {
                var employee = dbContext.employees.FirstOrDefault(e => e.EmployeeLogin == login);
                if (employee != null && PasswordHashing.VerifyPassword(password, employee.EmployeePassword))
                {
                    Window newWindow = new Window
                    {
                        Content = new MainContent(),
                        Title = "FlowMeter",
                    };

                    Window.GetWindow(this).Close();
                    newWindow.Show();
                }
                else
                {
                    this.textlogin.Text = string.Empty;
                    this.textpassword.Password = string.Empty;

                    // Display error message
                    ErrorMessage.Text = "Неправильний логін або пароль";
                    ErrorMessage.Visibility = Visibility.Visible;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {

                this.DragMove();
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void TextBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBoxLogin = (TextBox)sender;
            TextBlock loginBackgroundText = (TextBlock)textBoxLogin.Template.FindName("LoginBackgroundText", textBoxLogin);

            loginBackgroundText.Visibility = string.IsNullOrEmpty(textBoxLogin.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            TextBlock placeholderText = (TextBlock)passwordBox.Template.FindName("PlaceholderText", passwordBox);

            placeholderText.Visibility = string.IsNullOrEmpty(passwordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}