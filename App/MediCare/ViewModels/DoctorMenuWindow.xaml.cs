using System.Windows;

namespace MediCare.Views
{
    public partial class DoctorMenuWindow : Window
    {
        private readonly int _doctorId;

        public DoctorMenuWindow(int doctorId)
        {
            InitializeComponent();
            _doctorId = doctorId;
        }

        private void MyAppointments_Click(object sender, RoutedEventArgs e)
        {
            // var myAppointmentsWindow = new DoctorAppointmentsWindow(_doctorId);
            // myAppointmentsWindow.ShowDialog();
            MessageBox.Show("Funkcja 'Zaplanowane wizyty' jest w trakcie implementacji.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CreateReport_Click(object sender, RoutedEventArgs e)
        {
            // var reportWindow = new AppointmentReportWindow(_doctorId);
            // reportWindow.ShowDialog();
            MessageBox.Show("Funkcja 'Wystaw raport' jest w trakcie implementacji.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void HistoryAppo_Click(object sender, RoutedEventArgs e)
        {
            // var editWindow = new DoctorDataWindow(_doctorId);
            // editWindow.ShowDialog();
            MessageBox.Show("Funkcja 'Historia wizyt' jest w trakcie implementacji.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
