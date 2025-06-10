using MediCare.Views;
using System.Windows;

namespace MediCare.Views
{
    public partial class DoctorMenuWindow : Window
    {
        private readonly int _doctorId;
        private ResourceDictionary _plDict;
        private ResourceDictionary _enDict;
        private string _currentLang = "PL";

        public DoctorMenuWindow(int doctorId)
        {
            InitializeComponent();
            _doctorId = doctorId;
            _plDict = new ResourceDictionary { Source = new Uri("Data/Resources/pl/DoctorMenuWindow.pl.xaml", UriKind.Relative) };
            _enDict = new ResourceDictionary { Source = new Uri("Data/Resources/en/DoctorMenuWindow.en.xaml", UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(_plDict);
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
        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            var statWindow = new DoctorStatisticsWindow(_doctorId);
            statWindow.ShowDialog();
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

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
