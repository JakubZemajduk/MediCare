using MediCare.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Controls;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using System.IO;

namespace MediCare.Views
{
    public partial class ReportViewWindow : Window
    {
        private readonly DB_MediCareContext _context = new();

        public ReportViewWindow(int appointmentId)
        {
            InitializeComponent();
            LoadReport(appointmentId);
        }

        private void LoadReport(int appointmentId)
        {
            var report = _context.AppointmentReports
                .Include(r => r.Appointment)
                .FirstOrDefault(r => r.AppointmentId == appointmentId);

            if (report == null)
            {
                MessageBox.Show("Raport nie został odnaleziony.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
                return;
            }

            DiseaseTextBox.Text = report.Disease;
            DescriptionTextBox.Text = report.Description;
            PrescriptionTextBox.Text = report.Prescription;

            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == report.Appointment.DoctorId);
            if (doctor != null)
            {
                DoctorNameTextBlock.Text = doctor.FirstName + " " + doctor.LastName;

                var specialization = _context.Specializations.FirstOrDefault(s => s.Id == doctor.SpecializationId);
                SpecializationTextBlock.Text = specialization?.Name ?? "-";
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                var text = GetReportText();
                var flowDoc = new FlowDocument(new Paragraph(new Run(text)))
                {
                    FontSize = 14,
                    PageWidth = 600,
                    PageHeight = 800
                };

                flowDoc.Name = "ReportDoc";
                IDocumentPaginatorSource idpSource = flowDoc;
                printDialog.PrintDocument(idpSource.DocumentPaginator, "Raport z wizyty");
            }
        }
        private string GetReportText()
        {
            return $"LEKARZ: {DoctorNameTextBlock.Text}\n" +
                   $"SPECJALIZACJA: {SpecializationTextBlock.Text}\n\n" +
                   $"CHOROBA:\n{DiseaseTextBox.Text}\n\n" +
                   $"OPIS WIZYTY:\n{DescriptionTextBox.Text}\n\n" +
                   $"RECEPTA:\n{PrescriptionTextBox.Text}";
        }

        private void SavePdfButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            var document = new PdfDocument();
            document.Info.Title = "Raport z wizyty";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Times New Roman", 12);
            
            string reportText = GetReportText();
            gfx.DrawString(reportText, font, XBrushes.Black, new XRect(40, 40, page.Width - 80, page.Height - 80), XStringFormats.TopLeft);

            var savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Raport_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
            document.Save(savePath);

            MessageBox.Show("PDF zapisany na pulpicie:\n" + savePath, "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            Process.Start("explorer.exe", $"/select,\"{savePath}\"");
            */
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
