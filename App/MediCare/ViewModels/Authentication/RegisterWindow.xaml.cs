using MediCare.Data;
using MediCare.Data.Controllers;
using MediCare.Data.DTOs;
using MediCare.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public partial class RegisterWindow : Window
    {
        private ResourceDictionary _plDict;
        private ResourceDictionary _enDict;
        private string _currentLang = "PL";
        public RegisterWindow()
        {
            InitializeComponent();
            _plDict = new ResourceDictionary { Source = new Uri("Data/Resources/pl/RegisterWindow.pl.xaml", UriKind.Relative) };
            _enDict = new ResourceDictionary { Source = new Uri("Data/Resources/en/RegisterWindow.en.xaml", UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(_plDict);
        }
        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            await RegisterButton_ClickAsync(sender, e);
        }

        private async Task RegisterButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (!new EmailAddressAttribute().IsValid(EmailTextBox.Text))
            {
                MessageBox.Show("Nieprawidłowy adres e-mail.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PasswordBox.Password.Length < 8)
            {
                MessageBox.Show("Hasło musi mieć co najmniej 8 znaków.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(PasswordBox.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$"))
            {
                MessageBox.Show("Hasło musi zawierać co najmniej jedną małą literę, jedną dużą literę oraz jedną cyfrę.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PasswordBox.Password != RepeatPasswordBox.Password)
            {
                MessageBox.Show("Hasła nie są takie same.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

                var db = App.ServiceProvider.GetRequiredService<DB_MediCareContext>();
                var user = db.Users.FirstOrDefault(u => u.Email == dto.Email);

                if (user != null)
                {
                    var patientWindow = new PatientDataWindow(user.Id);
                    var result = patientWindow.ShowDialog();
                    if (result != true)
                    {
                        MessageBox.Show("Dane pacjenta nie zostały uzupełnione. Zostaniesz wylogowany.", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                        Application.Current.Shutdown();
                        return;
                    }

                    var db2 = App.ServiceProvider.GetRequiredService<DB_MediCareContext>();
                    var patient = db2.Patients.FirstOrDefault(p => p.UserId == user.Id);
                    if (patient != null)
                    {
                        var menuWindow = new PatientMenuWindow(patient.Id);
                        menuWindow.Show();
                    }
                }

                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LangButton_Click(object sender, RoutedEventArgs e)
        {
            this.Resources.MergedDictionaries.Clear();
            if (_currentLang == "PL")
            {
                this.Resources.MergedDictionaries.Add(_enDict);
                _currentLang = "EN";
                LangButton.Content = "EN";
            }
            else
            {
                this.Resources.MergedDictionaries.Add(_plDict);
                _currentLang = "PL";
                LangButton.Content = "PL";
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
