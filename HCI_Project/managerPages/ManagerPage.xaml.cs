using HCI_Project.utils;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project.managerPages
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        private readonly Frame MainFrame;
        public ManagerPage(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            Style = (Style)FindResource(typeof(Page));
        }

        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            Database.LogOut();
            MainFrame.Content = new LogInPage(MainFrame);
        }

        private void ShowNewTrainLine(object sender, RoutedEventArgs e)
        {
            ManagerFrame.Content = new NewTrainLinePage();
        }

        private void ShowNewTrain(object sender, RoutedEventArgs e)
        {
            ManagerFrame.Content = new NewTrainPage();
        }
    }
}
