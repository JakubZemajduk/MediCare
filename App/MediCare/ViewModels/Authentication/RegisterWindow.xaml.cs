using MediCare.Data.Controllers;
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
    // Logika interakcji dla klasy RegisterWindow.xaml
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        private async Task RegisterButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            var registerService = App.ServiceProvider.GetRequiredService<RegisterService>();
            var registerController = new RegisterController(registerService);

            var dto = new RegisterUserDto
            {
                Email = EmailTextBox.Text,
                Password = PasswordBox.Password
            };

            var (success, message) = await registerController.RegisterAsync(dto);

            if (success)
            {
                MessageBox.Show("Rejestracja zakończona sukcesem.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
