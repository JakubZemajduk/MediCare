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
            var document = new PdfDocument();
            document.Info.Title = "Raport z wizyty";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

          
            var titleFont = new XFont("Arial", 16);
            var textFont = new XFont("Arial", 12);

            double margin = 40;
            double y = margin;

            gfx.DrawString("MediCare – Raport z wizyty", titleFont, XBrushes.Black,
                new XRect(margin, y, page.Width - 2 * margin, 30), XStringFormats.TopLeft);
            y += 40;

            gfx.DrawString("LEKARZ:", textFont, XBrushes.Black, new XPoint(margin, y));
            y += 20;
            gfx.DrawString(DoctorNameTextBlock.Text, textFont, XBrushes.Black, new XPoint(margin, y));
            y += 30;

            gfx.DrawString("SPECJALIZACJA:", textFont, XBrushes.Black, new XPoint(margin, y));
            y += 20;
            gfx.DrawString(SpecializationTextBlock.Text, textFont, XBrushes.Black, new XPoint(margin, y));
            y += 30;

            gfx.DrawString("CHOROBA:", textFont, XBrushes.Black, new XPoint(margin, y));
            y += 20;
            gfx.DrawString(DiseaseTextBox.Text, textFont, XBrushes.Black,
                new XRect(margin, y, page.Width - 2 * margin, 80), XStringFormats.TopLeft);
            y += 80;

            gfx.DrawString("OPIS WIZYTY:", textFont, XBrushes.Black, new XPoint(margin, y));
            y += 20;
            gfx.DrawString(DescriptionTextBox.Text, textFont, XBrushes.Black,
                new XRect(margin, y, page.Width - 2 * margin, 100), XStringFormats.TopLeft);
            y += 100;

            gfx.DrawString("RECEPTA:", textFont, XBrushes.Black, new XPoint(margin, y));
            y += 20;
            gfx.DrawString(PrescriptionTextBox.Text, textFont, XBrushes.Black,
                new XRect(margin, y, page.Width - 2 * margin, 80), XStringFormats.TopLeft);

            var savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"Raport_{DateTime.Now:yyyyMMdd_HHmm}.pdf");

            document.Save(savePath);

            MessageBox.Show("PDF zapisany na pulpicie:\n" + savePath,
                "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

            Process.Start("explorer.exe", $"/select,\"{savePath}\"");
        }



        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
