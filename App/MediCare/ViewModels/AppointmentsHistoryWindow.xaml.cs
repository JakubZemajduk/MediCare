using MediCare.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediCare.Views
{
    public partial class AppointmentsHistoryWindow : Window
    {
        private readonly DB_MediCareContext _context = new();
        private readonly int _doctorId;

        public AppointmentsHistoryWindow(int doctorId)
        {
            InitializeComponent();
            _doctorId = doctorId;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == _doctorId && a.DateTime < DateTime.Now)
                .OrderByDescending(a => a.DateTime)
                .Select(a => new
                {
                    a.DateTime,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    a.Status,
                    AppointmentId = a.Id
                })
                .ToList();

            AppointmentsList.ItemsSource = appointments;
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is int appointmentId)
            {
                MessageBox.Show($"Podgląd/edycja raportu dla wizyty ID: {appointmentId}", "Raport", MessageBoxButton.OK, MessageBoxImage.Information);

            
                 var reportWindow = new ReportWindow(appointmentId);
                 reportWindow.ShowDialog();
            }
        }
    }
}
