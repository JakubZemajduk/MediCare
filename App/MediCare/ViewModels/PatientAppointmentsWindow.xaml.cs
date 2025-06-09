using MediCare.Data;
using MediCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediCare.Views
{
    public partial class PatientAppointmentsWindow : Window
    {
        private readonly DB_MediCareContext _context = new();
        private readonly int _patientId;
        private List<AppointmentDisplay> _allAppointments;
        private ResourceDictionary _plDict;
        private ResourceDictionary _enDict;
        private string _currentLang = "PL";

        public PatientAppointmentsWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            _plDict = new ResourceDictionary { Source = new Uri("Data/Resources/pl/PatientAppointmentWindow.pl.xaml", UriKind.Relative) };
            _enDict = new ResourceDictionary { Source = new Uri("Data/Resources/en/PatientAppointmentWindow.en.xaml", UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(_plDict);
            LoadAppointments();
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
        private void LoadAppointments()
        {
            var now = DateTime.Now;
            var reportIds = _context.AppointmentReports.Select(r => r.AppointmentId).ToHashSet();

            var appointments = _context.Appointments
               .Include(a => a.Doctor)
               .Where(a => a.PatientId == _patientId)
               .ToList();

            foreach (var appt in appointments)
            {
                if (appt.DateTime < now && appt.Status == AppointmentStatus.Planned)
                {
                    appt.Status = AppointmentStatus.Completed;
                }
            }
            _context.SaveChanges();

            _allAppointments = appointments
                .Select(a => new AppointmentDisplay
                {
                    AppointmentId = a.Id,
                    DateTime = a.DateTime,
                    DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
                    Specialization = a.Doctor.SpecializationId != null
                        ? _context.Specializations.FirstOrDefault(s => s.Id == a.Doctor.SpecializationId)?.Name ?? "-"
                        : "-",
                    Status = a.Status.ToString(),
                    HasReport = reportIds.Contains(a.Id)
                })
                .OrderBy(a => a.DateTime)
                .ToList();

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var selected = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var now = DateTime.Now;
            IEnumerable<AppointmentDisplay> filtered = _allAppointments;

            if (selected == "Przeszłe")
                filtered = filtered.Where(a => a.DateTime < now);
            else if (selected == "Przyszłe")
                filtered = filtered.Where(a => a.DateTime >= now);

            AppointmentsList.ItemsSource = filtered.ToList();
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded) ApplyFilter();
        }

        private void ShowReport_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is int appointmentId)
            {
                var reportWindow = new ReportViewWindow(appointmentId);
                reportWindow.ShowDialog();
            }
        }

        private void CancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is int appointmentId)
            {
                var appointment = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);

                if (appointment == null)
                {
                    MessageBox.Show("Nie znaleziono wizyty.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (appointment.DateTime <= DateTime.Now)
                {
                    MessageBox.Show("Nie można anulować przeszłej wizyty.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show("Czy na pewno chcesz anulować tę wizytę?", "Potwierdzenie",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    appointment.Status = AppointmentStatus.Cancelled;
                    _context.SaveChanges();
                    LoadAppointments();
                    MessageBox.Show("Wizyta została anulowana.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private class AppointmentDisplay
        {
            public int AppointmentId { get; set; }
            public DateTime DateTime { get; set; }
            public string DoctorName { get; set; }
            public string Specialization { get; set; }
            public string Status { get; set; }
            public bool HasReport { get; set; }
        }
    }
}
