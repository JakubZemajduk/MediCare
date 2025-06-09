using MediCare.Data;
using MediCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace MediCare.Views
{
    public partial class ReportWindow : Window
    {
        private readonly DB_MediCareContext _context = new();
        private readonly int _appointmentId;
        private AppointmentReport _existingReport;
        private ResourceDictionary _plDict;
        private ResourceDictionary _enDict;
        private string _currentLang = "PL";

        public ReportWindow(int appointmentId)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
            _plDict = new ResourceDictionary { Source = new Uri("Data/Resources/pl/ReportWindow.pl.xaml", UriKind.Relative) };
            _enDict = new ResourceDictionary { Source = new Uri("Data/Resources/en/ReportWindow.en.xaml", UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(_plDict);
            LoadReport();
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
        private void LoadReport()
        {
            _existingReport = _context.AppointmentReports
                .FirstOrDefault(r => r.AppointmentId == _appointmentId);

            if (_existingReport != null)
            {
               
                DiseaseTextBox.Text = _existingReport.Disease;
                DescriptionTextBox.Text = _existingReport.Description;
                PrescriptionTextBox.Text = _existingReport.Prescription;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var disease = DiseaseTextBox.Text.Trim();
            var description = DescriptionTextBox.Text.Trim();
            var prescription = PrescriptionTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(disease) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Wypełnij wymagane pola (choroba i opis).", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_existingReport != null)
            {
               
                _existingReport.Disease = disease;
                _existingReport.Description = description;
                _existingReport.Prescription = prescription;
                _context.AppointmentReports.Update(_existingReport);
            }
            else
            {
               
                var newReport = new AppointmentReport
                {
                    AppointmentId = _appointmentId,
                    Disease = disease,
                    Description = description,
                    Prescription = prescription
                };
                _context.AppointmentReports.Add(newReport);
            }

            _context.SaveChanges();

            MessageBox.Show("Raport zapisany.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
