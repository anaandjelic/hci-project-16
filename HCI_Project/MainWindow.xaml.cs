using HCI_Project.popups;
using HCI_Project.utils;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HCI_Project
{
    public delegate void Redirect();
    public partial class MainWindow : Window
    {
        private WindowState oldstate = WindowState.Normal;
        private bool fulled = false;
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            MainFrame.Content = new LogInPage(MainFrame);
            Database.AddUser("pera", "pera", "Pera", "Peric", UserRole.CLIENT);
            Database.AddUser("maja", "maja", "Maja", "Majic", UserRole.MANAGER);
            Database.AddTrain("Vrabac", 3, 10);
            Database.AddTrain("Roda", 2, 5);
            Database.AddTrain("Noj", 3, 7);
            Database.AddTrain("Gavran", 4, 8);
            List<Station> stations1 = new List<Station>()
            {
                new Station("Sombor", 0, 45.773979, 19.118759, new TimeSpan(0)),
                new Station("Vrbas", 200, 45.572021, 19.639780, new TimeSpan(1, 15, 0)),
                new Station("Novi Sad", 500, 45.254410, 19.842550, new TimeSpan(2, 0, 0)),
                new Station("Belgrade", 1000, 44.794043, 20.461006, new TimeSpan(3, 20, 0))
            };
            List<Station> stations2 = new List<Station>()
            {
                new Station("Subotica", 100, 46.094150, 19.66, new TimeSpan(0)),
                new Station("Novi Sad", 500, 45.254410, 19.842550, new TimeSpan(2, 0, 0)),
                new Station("Belgrade", 1000, 44.794043, 20.461006, new TimeSpan(3, 20, 0)),
                new Station("Valjevo", 1300, 44.273660, 19.884121, new TimeSpan(3, 30, 0))
            };
            List<Station> stations3 = new List<Station>()
            {
                new Station("Sabac", 100, 44.7553, 19.6923, new TimeSpan(0)),
                new Station("Zagreb", 450, 45.8150, 15.9819, new TimeSpan(1, 25, 0)),
                new Station("Belgrade", 1000, 44.794043, 20.461006, new TimeSpan(3, 20, 0)),
                new Station("Valjevo", 1300, 44.273660, 19.884121, new TimeSpan(3, 30, 0))
            };

            List<Station> stations4 = new List<Station>()
            {
                new Station("Loznica", 100, 44.5338, 19.2238, new TimeSpan(0)),
                new Station("Pristina", 450, 42.6629, 21.1655, new TimeSpan(1, 25, 0)),
                new Station("Belgrade", 640, 43.794043, 25.461006, new TimeSpan(2, 10, 0)),
                new Station("Valjevo", 980, 44.273660, 19.884121, new TimeSpan(3, 30, 0))
            };

            Database.AddTrainLine(stations1, Database.GetTrain(0));
            Database.AddTrainLine(stations2, Database.GetTrain(1));
            Database.AddTrainLine(stations3, Database.GetTrain(2));
            Database.AddTrainLine(stations4, Database.GetTrain(3));

            Database.AddConfiguration(Database.GetTrainLine(0), new TimeSpan(7, 0, 0), true, true, true, true, true, true, true);
            Database.AddConfiguration(Database.GetTrainLine(1), new TimeSpan(8, 30, 0), true, true, true, true, true, true, true);
            Database.AddConfiguration(Database.GetTrainLine(2), new TimeSpan(9, 30, 0), true, true, true, true, true, true, true);
            Database.AddConfiguration(Database.GetTrainLine(3), new TimeSpan(16, 0, 0), true, true, true, true, true, true, true);

            for (int i = -14; i < 14; i++)
            {
                Database.AddTimeTable(DateTime.Now.AddDays(i), Database.GetConfiguration(0));
                if (i % 2 == 0)
                    Database.AddTimeTable(DateTime.Now.AddDays(i), Database.GetConfiguration(1));
                if (i % 3 == 0)
                    Database.AddTimeTable(DateTime.Now.AddDays(i), Database.GetConfiguration(2));
                if (i % 4 != 0)
                    Database.AddTimeTable(DateTime.Now.AddDays(i), Database.GetConfiguration(3));
            }
        }

        private void FullScreen_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FullScreen_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (!fulled)
            {
                this.WindowState = WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;
                fulled = true;
            }
            else
            {
                this.WindowState = oldstate;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                fulled = false;
            }
        }
    }
}
