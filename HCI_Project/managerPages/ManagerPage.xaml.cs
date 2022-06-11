using HCI_Project.help;
using HCI_Project.utils;
using System;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace HCI_Project.managerPages
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        private readonly Frame MainFrame;
        private bool isTutorialCreateTrain = false;
        private bool startedCreatingTrain = false;

        private bool isTutorialCreateTrainLine = false;
        private bool startedCreatingTrainLine = false;

        public ManagerPage(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            Style = (Style)FindResource(typeof(Page));
            ManagerFrame.Focus();
        }

        private void CreateTrain_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (!startedCreatingTrain && !isTutorialCreateTrainLine)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void CreateTrain_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ManagerFrame.Content = new NewTrainPage(isTutorialCreateTrain,MainFrame,notifier);
            ManagerFrame.Focus();
            if (isTutorialCreateTrain)
                this.startedCreatingTrain = true;
        }

        private void CreateTrainLine_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if(isTutorialCreateTrain && !startedCreatingTrainLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void CreateTrainLine_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ManagerFrame.Content = new NewTrainLinePage(isTutorialCreateTrainLine, MainFrame, notifier);
            ManagerFrame.Focus();
            if (isTutorialCreateTrainLine)
                this.startedCreatingTrainLine = true;
        }

        private void CreateTrainTable_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialCreateTrain || isTutorialCreateTrainLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void CreateTrainTable_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ManagerFrame.Content = new NewTimeTablePage();
            ManagerFrame.Focus();
        }

        private void LogOut_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialCreateTrain || isTutorialCreateTrainLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void LogOut_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Database.LogOut();
            MainFrame.Content = new LogInPage(MainFrame);
        }

        private void EditTrain_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialCreateTrain || isTutorialCreateTrainLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void EditTrain_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ManagerFrame.Content = new EditTrainPage();
            ManagerFrame.Focus();
        }

        private void EditTrainLine_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialCreateTrain || isTutorialCreateTrainLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void EditTrainLine_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Edit Train line in development");
        }

        private void EditTrainTable_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialCreateTrain || isTutorialCreateTrainLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void EditTrainTable_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ManagerFrame.Content = new EditTimeTablePage();
            ManagerFrame.Focus();
        }
        

        private void MonthlyReports_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialCreateTrain || isTutorialCreateTrainLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void MonthlyReports_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(" MonthlyReports in development");
            ManagerFrame.Focus();
        }
        private void PerTableReports_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialCreateTrain || isTutorialCreateTrainLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void PerTableReports_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(" PerTableReports in development");
            ManagerFrame.Focus();
        }

        private void ManagerHelp_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (isTutorialCreateTrain || isTutorialCreateTrainLine)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void ManagerHelp_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            new HelpViewer("manager.html").ShowDialog();
        }

        private void CreateTrainTutorial_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (!isTutorialCreateTrain && !isTutorialCreateTrainLine)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void CreateTrainTutorial_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            isTutorialCreateTrain = true;
            var optionsMax = new MessageOptions
            {
                FontSize = 25,
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true
            };

            string message = "This begins the Train creating Tutorial\nTo continue press Ctrl C,T or go to New -> Train";
            this.notifier.ShowWarning(message, optionsMax);
            this.editItem.IsEnabled = false;
            this.helpItem.IsEnabled = false;
            this.mainItem.IsEnabled = false;
            this.reportsItem.IsEnabled = false;
        }

       
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

            cfg.DisplayOptions.Width = Application.Current.MainWindow.Width / 3;
        });

        private void CreateTrainLineTutorial_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (!isTutorialCreateTrain && !isTutorialCreateTrainLine)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void CreateTrainLineTutorial_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            isTutorialCreateTrainLine = true;
            var optionsMax = new MessageOptions
            {
                FontSize = 25,
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true
            };

            string message = "This begins the Train Line creating Tutorial\nTo continue press Ctrl C,L or go to New -> TrainLine";
            this.notifier.ShowWarning(message, optionsMax);
            this.editItem.IsEnabled = false;
            this.helpItem.IsEnabled = false;
            this.mainItem.IsEnabled = false;
            this.reportsItem.IsEnabled = false;
        }
    }
}
