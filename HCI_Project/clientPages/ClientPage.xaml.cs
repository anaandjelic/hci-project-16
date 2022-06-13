using HCI_Project.utils;
using System;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using ToastNotifications.Messages.Core;
using ToastNotifications.Core;
using HCI_Project.help;
using HCI_Project.popups;

namespace HCI_Project.clientPages
{
    /// <summary>
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        private readonly Frame MainFrame;
        private bool isTutorialLine=false;
        private bool stardedSearchLine = false;

        private bool enableTutorial = true;

        public ClientPage(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            Style = (Style)FindResource(typeof(Page));
            ClientFrame.Focus();
         
        }

        private void SearchTrainLine_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if(!stardedSearchLine)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void SearchTrainLine_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            // ovde bih prosledio da je u pitanju tutorial 
            //ClientFrame.Content = new MapPage();
            ClientFrame.Content = new MapPage(isTutorialLine,MainFrame,notifier);
            enableTutorial = false;
            ClientFrame.Focus();
            if(isTutorialLine)
                this.stardedSearchLine = true;
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
            enableTutorial = false;
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
            enableTutorial = false;
            ClientFrame.Focus();
        }

        private void HistoryReservations_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            // ClientFrame.Content = new ReservationsPage();
            MessageBox.Show("Reservation window is development");
        }

        private void Tutorial_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if(!isTutorialLine && enableTutorial)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void Tutorial_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            isTutorialLine = true;
            //MessageBox.Show("This begins the Train Line Search Tutorial\nTo continue press Ctrl F,L or go to Search -> TrainLine");

            var optionsMax = new MessageOptions
            {
                FontSize = 30,
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true            
            };

            string message = "This begins the Train Line Search Tutorial\nTo continue press Ctrl F,L or go to Search -> TrainLine\n To exit the Tutorial press Ctrl T,X";
            this.notifier.ShowWarning(message,optionsMax);
            this.HistoryBTN.IsEnabled = false;
            this.MainBar.IsEnabled = false;
            this.TimeTableBtn.IsEnabled = false;

            // ako su ovi koraci obavljeni onda skacemo na searchtrainline prozor
            // tu i dalje ostaje ovo stanje sa menu itemima, a korisnika teramo da unese npr beograd u search bar i da pritisne enter za pretragu
        }

        private void Purchase_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void Purchase_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientFrame.Content = new PurchasePage(ClientFrame);
            enableTutorial = false;
            ClientFrame.Focus();
        }

        private void Reserve_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void Reserve_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientFrame.Content = new SearchPage();
            enableTutorial = false;
            ClientFrame.Focus();
        }

        //  NOTIFIKACIJE (zamena za msgbox) 
        // vrv mogu da ga prosledjujem po pageovima za njegovo prikazivanje
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 15,
                offsetY: 15);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(6),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = Application.Current.MainWindow.Width /3;
        });



        private void ClientHelp_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void ClientHelp_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            new HelpViewer("client.html").ShowDialog();
            enableTutorial = false;
        }

        private void CancelTutorial_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialLine)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void CancelTutorial_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var optionsMax = new MessageOptions
            {
                FontSize = 30,
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true
            };

            string message = "This will end the Tutorial";
            this.notifier.ShowWarning(message, optionsMax);
            //MessageBox.Show("Klikom na ok se vracate na login screen");
            new MessageBoxCustom("By clicking the ok button you will be returned to the last page you were on", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
            this.NavigationService.GoBack();
        }
    }
}
