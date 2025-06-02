using MediCare.Views;
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
            var upcomingappointmentswindow = new AppointmentsUpcomingWindow(_doctorId);
            upcomingappointmentswindow.ShowDialog();
           
        }

        

        private void HistoryAppo_Click(object sender, RoutedEventArgs e)
        {
            var myAppointmentsWindow = new AppointmentsHistoryWindow(_doctorId);
            myAppointmentsWindow.ShowDialog();
            
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
