using System.Windows;

namespace MediCare
{
    public partial class MainWindow : Window
    {
        private ResourceDictionary _plDict;
        private ResourceDictionary _enDict;
        private string _currentLang = "PL";

        public MainWindow()
        {
            InitializeComponent();
            _plDict = new ResourceDictionary { Source = new Uri("Data/Resources/pl/MainWindow.pl.xaml", UriKind.Relative) };
            _enDict = new ResourceDictionary { Source = new Uri("Data/Resources/en/MainWindow.en.xaml", UriKind.Relative) };
            this.Resources.MergedDictionaries.Add(_plDict);
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
            //var doctorsWindow = new Views.DoctorsWindow();
            //doctorsWindow.ShowDialog();
            MessageBox.Show("Trzeba zaimplementować", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
