using System.Windows;

namespace MediCare.Views
{
    public partial class PatientMenuWindow : Window
    {
        private readonly int _patientId;

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
    }
}
