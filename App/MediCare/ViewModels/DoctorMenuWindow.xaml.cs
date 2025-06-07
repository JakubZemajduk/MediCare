using MediCare.Views;
using System.Windows;

namespace MediCare.Views
{
    public partial class DoctorMenuWindow : Window
    {
        private readonly int _doctorId;
        private string _currentLang = "PL";

        public DoctorMenuWindow(int doctorId)
        {
            InitializeComponent();
            _doctorId = doctorId;
        }

        private void MyAppointments_Click(object sender, RoutedEventArgs e)
        {
            var upcomingappointmentswindow = new AppointmentsUpcomingWindow(_doctorId);
            upcomingappointmentswindow.ShowDialog();       
        }

        private void HistoryAppo_Click(object sender, RoutedEventArgs e)
        {
            var myAppointmentsWindow = new AppointmentsHistoryWindow(_doctorId);
            myAppointmentsWindow.ShowDialog();
            
        }

        private void ReportAbsence_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funkcja 'Zgłoś nieobecność' jest w trakcie implementacji.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ChangeLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentLang == "PL")
            {
                _currentLang = "EN";
                LangButton.Content = "EN";
                MessageBox.Show("English version coming soon!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _currentLang = "PL";
                LangButton.Content = "PL";
                MessageBox.Show("Wersja polska!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
