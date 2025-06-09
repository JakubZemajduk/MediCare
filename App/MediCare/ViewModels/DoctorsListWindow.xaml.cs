using MediCare.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediCare.Views
{
    public partial class DoctorsListWindow : Window
    {
        private readonly DB_MediCareContext _context = new();
        private List<DoctorDisplay> _allDoctors = new();
        private bool _placeholderActive = true;
        private ResourceDictionary _plDict;
        private ResourceDictionary _enDict;
        private string _currentLang = "PL";

        public DoctorsListWindow()
        {
            InitializeComponent();
            _plDict = new ResourceDictionary { Source = new Uri("Data/Resources/pl/DoctorsListWindow.pl.xaml", UriKind.Relative) };
            _enDict = new ResourceDictionary { Source = new Uri("Data/Resources/en/DoctorsListWindow.en.xaml", UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(_plDict);
            LoadSpecializations();
            LoadDoctors();
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

        private void LoadSpecializations()
        {
            var specializations = _context.Specializations.Select(s => s.Name).ToList();
            SpecializationComboBox.ItemsSource = new[] { "Wszystkie" }.Concat(specializations);
            SpecializationComboBox.SelectedIndex = 0;
        }

        private void LoadDoctors()
        {
            _allDoctors = _context.Doctors
                .Include(d => d.Specialization)
                .Select(d => new DoctorDisplay
                {
                    FullName = d.FirstName + " " + d.LastName,
                    PhoneNumber = d.PhoneNumber,
                    Specialization = d.Specialization.Name
                })
                .OrderBy(d => d.FullName)
                .ToList();

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var nameFilter = NameTextBox.Text.ToLower();
            var selectedSpecialization = SpecializationComboBox.SelectedItem?.ToString();

            if (_placeholderActive) nameFilter = "";

            var filtered = _allDoctors.Where(d =>
                d.FullName.ToLower().Contains(nameFilter) &&
                (selectedSpecialization == "Wszystkie" || d.Specialization == selectedSpecialization)
            ).ToList();

            DoctorsList.ItemsSource = filtered;
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_placeholderActive)
                ApplyFilter();
        }

        private void SpecializationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilter();

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_placeholderActive)
            {
                NameTextBox.Text = "";
                NameTextBox.Foreground = Brushes.Black;
                _placeholderActive = false;
            }
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                NameTextBox.Text = "Szukaj po nazwisku...";
                NameTextBox.Foreground = Brushes.Gray;
                _placeholderActive = true;
            }
        }
    }

    public class DoctorDisplay
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
    }
}
