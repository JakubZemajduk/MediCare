using System.Windows;

namespace MediCare
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new Views.Authentication.RegisterWindow();
            registerWindow.ShowDialog();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new Views.Authentication.LoginWindow();
            loginWindow.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
