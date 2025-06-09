using MediCare.Data;
using MediCare.Data.DTOs;
using MediCare.Data.Models;
using MediCare.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MediCare.Views.Authentication
{
    public partial class LoginWindow : Window
    {
        private ResourceDictionary _plDict;
        private ResourceDictionary _enDict;
        private string _currentLang = "PL";
        public LoginWindow()
        {
            InitializeComponent();
            _plDict = new ResourceDictionary { Source = new Uri("Data/Resources/pl/LoginWindow.pl.xaml", UriKind.Relative) };
            _enDict = new ResourceDictionary { Source = new Uri("Data/Resources/en/LoginWindow.en.xaml", UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(_plDict);
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginService = App.ServiceProvider.GetRequiredService<LoginService>();
            var dto = new LoginUserDto
            {
                Email = EmailTextBox.Text,
                Password = PasswordBox.Password
            };

            var (success, errorMessage, user) = await loginService.LoginAsync(dto);

            if (success && user != null)
            {
                var db = App.ServiceProvider.GetRequiredService<DB_MediCareContext>();

                if (user.Role == UserRole.Patient)
                {
                    var patient = db.Patients.FirstOrDefault(p => p.UserId == user.Id);
                    if (patient == null)
                    {
                        MessageBox.Show("Musisz uzupełnić dane pacjenta, aby korzystać z aplikacji.", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                        var patientWindow = new PatientDataWindow(user.Id);
                        var result = patientWindow.ShowDialog();

                        if (result != true)
                        {
                            MessageBox.Show("Dane pacjenta nie zostały uzupełnione. Zostaniesz wylogowany.", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                            Application.Current.Shutdown();
                            return;
                        }

                        patient = db.Patients.FirstOrDefault(p => p.UserId == user.Id);
                        if (patient == null)
                        {
                            MessageBox.Show("Wystąpił błąd podczas pobierania danych pacjenta.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                            Application.Current.Shutdown();
                            return;
                        }
                    }
                    MessageBox.Show("Zalogowano pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    var menuWindow = new PatientMenuWindow(patient.Id);
                    menuWindow.Show();
                    this.Close();
                }
                else if (user.Role == UserRole.Doctor)
                {
                    var doctor = db.Doctors.FirstOrDefault(d => d.UserId == user.Id);
                    if (doctor == null)
                    {
                        MessageBox.Show("Nie znaleziono danych lekarza.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    MessageBox.Show("Zalogowano pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    var menuWindow = new DoctorMenuWindow(doctor.Id);
                    menuWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nieznana rola użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(errorMessage, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
