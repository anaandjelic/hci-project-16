using HCI_Project.help;
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
            ManagerFrame.Focus();
        }

        private void CreateTrain_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CreateTrain_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ManagerFrame.Content = new NewTrainPage();
            ManagerFrame.Focus();
        }

        private void CreateTrainLine_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CreateTrainLine_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ManagerFrame.Content = new NewTrainLinePage();
            ManagerFrame.Focus();
        }

        private void CreateTrainTable_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CreateTrainTable_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ManagerFrame.Content = new NewTimeTablePage();
            ManagerFrame.Focus();
        }

        private void LogOut_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void LogOut_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Database.LogOut();
            MainFrame.Content = new LogInPage(MainFrame);
        }

        private void EditTrain_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EditTrain_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ManagerFrame.Content = new EditTrainPage();
            ManagerFrame.Focus();
        }

        private void EditTrainLine_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EditTrainLine_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Edit Train line in development");
        }

        private void EditTrainTable_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EditTrainTable_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ManagerFrame.Content = new EditTimeTablePage();
            ManagerFrame.Focus();
        }
        

        private void MonthlyReports_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MonthlyReports_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(" MonthlyReports in development");
            ManagerFrame.Focus();
        }
        private void PerTableReports_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PerTableReports_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(" PerTableReports in development");
            ManagerFrame.Focus();
        }

        private void ManagerHelp_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ManagerHelp_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            new HelpViewer("manager.html").ShowDialog();
        }
    }
}
