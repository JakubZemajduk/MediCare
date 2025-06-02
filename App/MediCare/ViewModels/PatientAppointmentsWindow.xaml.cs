using MediCare.Data;
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

        public PatientAppointmentsWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            var reportIds = _context.AppointmentReports.Select(r => r.AppointmentId).ToHashSet();

            _allAppointments = _context.Appointments
               .Include(a => a.Doctor)
                .Where(a => a.PatientId == _patientId)
                .ToList()
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
