using System.Windows;

namespace MediCare.Views
{
    public partial class PatientMenuWindow : Window
    {
        private readonly int _patientId;
        private ResourceDictionary _plDict;
        private ResourceDictionary _enDict;
        private string _currentLang = "PL";

        public PatientMenuWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            _plDict = new ResourceDictionary { Source = new Uri("Data/Resources/pl/PatientMenuWindow.pl.xaml", UriKind.Relative) };
            _enDict = new ResourceDictionary { Source = new Uri("Data/Resources/en/PatientMenuWindow.en.xaml", UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(_plDict);
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
         var doctorsListWindow = new DoctorsListWindow();
            doctorsListWindow.ShowDialog();
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
    }
}
