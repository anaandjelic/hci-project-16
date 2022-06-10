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
        private bool isTutorialLine=false;

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
            // ovde bih prosledio da je u pitanju tutorial 
            //ClientFrame.Content = new MapPage();
            ClientFrame.Content = new MapPage(isTutorialLine,MainFrame);
            ClientFrame.Focus();

        }

        private void SearchTrainTable_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if(isTutorialLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void SearchTrainTable_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientFrame.Content = new SearchPage();
            ClientFrame.Focus();
        }

        private void LogOut_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void HistoryTickets_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void HistoryReservations_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialLine)
                e.CanExecute = false;
            else
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

        private void Tutorial_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Tutorial_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            isTutorialLine = true;
            MessageBox.Show("This begins the Train Line Search Tutorial\nTo continue press Ctrl F,L or go to Search -> TrainLine");
            this.HistoryBTN.IsEnabled = false;
            this.MainBar.IsEnabled = false;
            this.TimeTableBtn.IsEnabled = false;

            // ako su ovi koraci obavljeni onda skacemo na searchtrainline prozor
            // tu i dalje ostaje ovo stanje sa menu itemima, a korisnika teramo da unese npr beograd u search bar i da pritisne enter za pretragu
        }
    }
}
