using System.Windows;

namespace MediCare
{
    public partial class MainWindow : Window
    {
        private string _currentLang = "PL";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new Views.Authentication.LoginWindow();
            loginWindow.ShowDialog();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new Views.Authentication.RegisterWindow();
            registerWindow.ShowDialog();
        }

        private void Doctors_Click(object sender, RoutedEventArgs e)
        {
            var doctorsWindow = new Views.DoctorsListWindow();
            doctorsWindow.ShowDialog();
           
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ChangeLanguage_Click(object sender, RoutedEventArgs e)
        {
            // Przełączanie języka (na razie tylko zmiana napisu)
            if (_currentLang == "PL")
            {
                _currentLang = "EN";
                LangButton.Content = "EN";
                MessageBox.Show("English version coming soon!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                // Tu w przyszłości: przełączanie ResourceDictionary lub CultureInfo
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
