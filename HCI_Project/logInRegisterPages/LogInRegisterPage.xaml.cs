using System;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project.LogInRegister
{
    public partial class LogInRegisterPage : Page
    {
        private event Redirect redirect;
        private event Redirect switchToLogIn;
        public LogInRegisterPage(Redirect logInRedirect)
        {
            InitializeComponent();
            redirect = logInRedirect;
            switchToLogIn += SwitchToLogIn;
            LogInRegisterFrame.Content = new LogIn(logInRedirect);
        }

        private void SwitchToLogIn()
        {
            LogInRegisterFrame.Content = new LogIn(redirect);
        }

        private void LogInPageClick(object sender, RoutedEventArgs e)
        {
            LogInRegisterFrame.Content = new LogIn(redirect);
        }

        private void RegisterPageClick(object sender, RoutedEventArgs e)
        {
            LogInRegisterFrame.Content = new Registration(switchToLogIn);
        }
    }
}
