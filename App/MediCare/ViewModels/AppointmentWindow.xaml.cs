using MediCare.Data;
using MediCare.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediCare.Views
{
    public partial class AppointmentWindow : Window
    {
        private readonly DB_MediCareContext _db;
        private int _patientId;
        private TimeSpan? SelectedHour = null;

        public AppointmentWindow(int patientId)
        {
            InitializeComponent();
            _db = App.ServiceProvider.GetRequiredService<DB_MediCareContext>();
            _patientId = patientId;
            LoadSpecializations();
        }

        private void LoadSpecializations()
        {
            var specializationIds = _db.Doctors
                .Select(d => d.SpecializationId)
                .Distinct()
                .ToList();

            var specializations = _db.Specializations
                .Where(s => specializationIds.Contains(s.Id))
                .OrderBy(s => s.Name)
                .ToList();

            SpecializationComboBox.ItemsSource = specializations;
            SpecializationComboBox.DisplayMemberPath = "Name";
            SpecializationComboBox.SelectedValuePath = "Id";
        }

        private void SpecializationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoctorComboBox.ItemsSource = null;
            Calendar.SelectedDate = null;
            HourWrapPanel.Children.Clear();
            SelectedHour = null;

            if (SpecializationComboBox.SelectedItem is Specialization specialization)
            {
                var doctors = _db.Doctors
                    .Where(d => d.SpecializationId == specialization.Id)
                    .Select(d => new
                    {
                        d.Id,
                        FullName = d.FirstName + " " + d.LastName
                    })
                    .ToList();

                DoctorComboBox.ItemsSource = doctors;
            }
        }

        private void DoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar.SelectedDate = null;
            HourWrapPanel.Children.Clear();
            SelectedHour = null;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            HourWrapPanel.Children.Clear();
            SelectedHour = null;

            if (DoctorComboBox.SelectedItem == null || Calendar.SelectedDate == null)
                return;

            var doctorId = (int)((dynamic)DoctorComboBox.SelectedItem).Id;
            var date = Calendar.SelectedDate.Value.Date;

            var allHours = Enumerable.Range(8, 8)
                .Select(h => new DateTime(date.Year, date.Month, date.Day, h, 0, 0))
                .ToList();

            var busyHours = _db.Appointments
                .Where(a => a.DoctorId == doctorId
                            && a.DateTime.Date == date
                            && a.Status == AppointmentStatus.Planned)
                .Select(a => a.DateTime)
                .ToList();

            var availableHours = allHours
                .Where(h => !busyHours.Any(b => b == h))
                .ToList();

            foreach (var hour in availableHours)
            {
                var btn = new Button
                {
                    Content = hour.ToString("HH:mm"),
                    Style = (Style)FindResource("HourButtonStyle"),
                    Tag = hour.TimeOfDay
                };
                btn.Click += (s, ev) =>
                {
                    foreach (Button b in HourWrapPanel.Children)
                        b.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#E0E0E0");
                    btn.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#4CAF50");
                    SelectedHour = (TimeSpan)btn.Tag;
                };
                HourWrapPanel.Children.Add(btn);
            }
        }

        private async void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorComboBox.SelectedItem == null || Calendar.SelectedDate == null || SelectedHour == null)
            {
                MessageBox.Show("Wybierz specjalizację, lekarza, datę i godzinę.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var doctorId = (int)((dynamic)DoctorComboBox.SelectedItem).Id;
            var date = Calendar.SelectedDate.Value.Date;
            var appointmentDateTime = date.Add(SelectedHour.Value);

            var isBusy = _db.Appointments.Any(a =>
                a.DoctorId == doctorId &&
                a.DateTime == appointmentDateTime &&
                a.Status == AppointmentStatus.Planned);

            if (isBusy)
            {
                MessageBox.Show("Wybrana godzina jest już zajęta.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                Calendar_SelectedDatesChanged(null, null);
                return;
            }

            var appointment = new Appointment
            {
                PatientId = _patientId,
                DoctorId = doctorId,
                DateTime = appointmentDateTime,
                Status = AppointmentStatus.Planned
            };

            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();

            MessageBox.Show("Wizyta została umówiona.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }

        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            BlockWeekends();
        }

        private void Calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            BlockWeekends();
        }

        private void BlockWeekends()
        {
            if (Calendar == null) return;

            Calendar.BlackoutDates.Clear();

            DateTime start = DateTime.Now.Date;
            DateTime end = start.AddYears(1);

            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    Calendar.BlackoutDates.Add(new CalendarDateRange(date));
                }
            }
        }
    }
}
