using LiveCharts;
using LiveCharts.Wpf;
using MediCare.Data;
using MediCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace MediCare.Views
{
    public partial class DoctorStatisticsWindow : Window
    {
        public SeriesCollection PatientsPerDoctorSeries { get; set; }
        public List<string> DoctorLabels { get; set; }

        private readonly DB_MediCareContext _context = new();
        private readonly int _doctorId;

        public DoctorStatisticsWindow(int doctorId)
        {
            InitializeComponent();
            _doctorId = doctorId;

            LoadPatientsPerDoctorChart();
            DataContext = this;
        }

        private void LoadPatientsPerDoctorChart()
        {
            var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var doctorPatientCounts = _context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.DateTime >= monthStart && a.DateTime < monthEnd)
                .GroupBy(a => new { a.DoctorId, a.Doctor.FirstName, a.Doctor.LastName })
                .Select(g => new
                {
                    DoctorName = g.Key.FirstName + " " + g.Key.LastName,
                    Count = g.Select(a => a.PatientId).Distinct().Count() // unikalni pacjenci
                })
                .OrderByDescending(g => g.Count)
                .ToList();

            DoctorLabels = doctorPatientCounts.Select(g => g.DoctorName).ToList();

            PatientsPerDoctorSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Pacjenci",
                    Values = new ChartValues<int>(doctorPatientCounts.Select(g => g.Count))
                }
            };
        }
    }
}
