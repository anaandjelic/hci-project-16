using HCI_Project.clientPages;
using HCI_Project.managerPages;
using HCI_Project.utils;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project
{
    public partial class LogInPage : Page
    {
        private readonly Frame MainFrame;
        public LogInPage(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            Style = (Style)FindResource(typeof(Page));
        }

        private void LogInClick(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                UserRole role = Database.LogIn(Username.Text, Password.Password);
                MainFrame.Content = role == UserRole.CLIENT ? new ClientPage(MainFrame) : (object)new ManagerPage(MainFrame);
            }
            catch (UserNotFoundException)
            {

            }
        }
    }
}
