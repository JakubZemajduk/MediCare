using System.Windows;

namespace MediCare.Views
{
    public partial class PatientMenuWindow : Window
    {
        private readonly int _patientId;
        private string _currentLang = "PL";

        public PatientMenuWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
        }

        private void ScheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            var appointmentWindow = new AppointmentWindow(_patientId);
            appointmentWindow.ShowDialog();
        }

        private void MyAppointments_Click(object sender, RoutedEventArgs e)
        {
             var myAppointmentsWindow = new PatientAppointmentsWindow(_patientId);
            myAppointmentsWindow.ShowDialog();
            

        }
        private void Doctors_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funkcja 'Nasi lekarze' jest w trakcie implementacji.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditData_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new PatientDataWindow(_patientId);
            editWindow.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
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
    }
}
