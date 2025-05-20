using MediCare.Data.DTOs;
using MediCare.Data.Services;
using Microsoft.Extensions.DependencyInjection;
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

namespace MediCare.Views.Authentication
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginService = App.ServiceProvider.GetRequiredService<LoginService>();
            var dto = new LoginUserDto
            {
                Email = EmailTextBox.Text,
                Password = PasswordBox.Password
            };

            var (success, errorMessage, user) = await loginService.LoginAsync(dto);

            if(success)
            {
                MessageBox.Show("Zalogowano pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
