using MediCare.Data;
using MediCare.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace MediCare.Views
{
    public partial class PatientDataWindow : Window
    {
        private readonly int _userId;
        private readonly DB_MediCareContext _db;
        private Patient _patient;

        public GenderType SelectedGender { get; set; } = GenderType.Male;

        public PatientDataWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            _db = App.ServiceProvider.GetRequiredService<DB_MediCareContext>();

            _patient = _db.Patients.FirstOrDefault(p => p.UserId == _userId);
            if (_patient != null)
            {
                FirstNameTextBox.Text = _patient.FirstName;
                LastNameTextBox.Text = _patient.LastName;
                PeselTextBox.Text = _patient.Pesel;
                CityTextBox.Text = _patient.City;
                StreetTextBox.Text = _patient.Street;
                PhoneNumberTextBox.Text = _patient.PhoneNumber;
                DateOfBirthPicker.SelectedDate = _patient.DateOfBirth;
                SelectedGender = _patient.Gender;
                GenderComboBox.SelectedItem = _patient.Gender;
            }

            DataContext = this;
        }
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Walidacja danych
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PeselTextBox.Text) ||
                string.IsNullOrWhiteSpace(CityTextBox.Text) ||
                string.IsNullOrWhiteSpace(StreetTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) ||
                !DateOfBirthPicker.SelectedDate.HasValue ||
                GenderComboBox.SelectedItem == null)
            {
                MessageBox.Show("Wszystkie pola są wymagane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(PeselTextBox.Text, @"^\d{11}$"))
            {
                MessageBox.Show("PESEL musi składać się z 11 cyfr.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!new PhoneAttribute().IsValid(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Nieprawidłowy numer telefonu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DateOfBirthPicker.SelectedDate.Value > DateTime.Now)
            {
                MessageBox.Show("Data urodzenia nie może być z przyszłości.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var patient = new Patient
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                Pesel = PeselTextBox.Text,
                City = CityTextBox.Text,
                Street = StreetTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                DateOfBirth = DateOfBirthPicker.SelectedDate.Value,
                Gender = SelectedGender,
                UserId = _userId
            };

            try
            {
                _db.Patients.Add(patient);
                await _db.SaveChangesAsync();
                MessageBox.Show("Dane pacjenta zostały zapisane.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas zapisu danych: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
