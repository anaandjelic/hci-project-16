using HCI_Project.utils;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project.clientPages
{
    /// <summary>
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        private readonly Frame MainFrame;

        public ClientPage(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            Style = (Style)FindResource(typeof(Page));
            ClientFrame.Focus();
         
        }

        private void SearchTrainLine_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SearchTrainLine_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientFrame.Content = new MapPage();
            ClientFrame.Focus();
        }

        private void SearchTrainTable_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SearchTrainTable_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientFrame.Content = new SearchPage();
            ClientFrame.Focus();
        }

        private void LogOut_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void HistoryTickets_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void HistoryReservations_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void LogOut_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Database.LogOut();
            MainFrame.Content = new LogInPage(MainFrame);
        }

        private void HistoryTickets_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientFrame.Content = new TicketsPage();
            ClientFrame.Focus();
        }

        private void HistoryReservations_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            // ClientFrame.Content = new ReservationsPage();
            MessageBox.Show("Reservation window is development");
        }
    }
}
