using HCI_Project.utils;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HCI_Project
{
    public delegate void Redirect();
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            MainFrame.Content = new LogInPage(MainFrame);
            Database.AddUser("pera", "pera", "Pera", "Peric", UserRole.CLIENT);
            Database.AddUser("maja", "maja", "Maja", "Majic", UserRole.MANAGER);
            Database.AddTrain(5, "Vrabac");
            Database.AddTrain(2, "Roda");
            Database.AddTrain(10, "Noj");
            Database.AddTrain(7, "Gavran");
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
                new Station("Novi Sad", 450, 45.254410, 19.842550, new TimeSpan(1, 25, 0)),
                new Station("Belgrade", 640, 44.794043, 20.461006, new TimeSpan(2, 10, 0)),
                new Station("Valjevo", 980, 44.273660, 19.884121, new TimeSpan(3, 30, 0))
            };
            List<Station> stations3 = new List<Station>()
            {
                new Station("Sabac", 100, 49.094150, 13.66, new TimeSpan(0)),
                new Station("Zagreb", 450, 49.254410, 13.842550, new TimeSpan(1, 25, 0)),
                new Station("Belgrade", 640, 49.794043, 23.461006, new TimeSpan(2, 10, 0)),
                new Station("Valjevo", 980, 49.273660, 13.884121, new TimeSpan(3, 30, 0))
            };

            List<Station> stations4 = new List<Station>()
            {
                new Station("Loznica", 100, 43.094150, 15.66, new TimeSpan(0)),
                new Station("Pristina", 450, 43.254410, 15.842550, new TimeSpan(1, 25, 0)),
                new Station("Belgrade", 640, 43.794043, 25.461006, new TimeSpan(2, 10, 0)),
                new Station("Valjevo", 980, 43.273660, 15.884121, new TimeSpan(3, 30, 0))
            };

            Database.AddTrainLine(stations1, Database.GetTrain(0));
            Database.AddTrainLine(stations2, Database.GetTrain(0));
            Database.AddTrainLine(stations3, Database.GetTrain(0));
            Database.AddTrainLine(stations4, Database.GetTrain(0));
            Database.AddTimeTable(DateTime.Now.AddMinutes(12), DateTime.Now.AddHours(3), Database.GetTrainLine(0));
            Database.AddTimeTable(DateTime.Now, DateTime.Now.AddHours(1), Database.GetTrainLine(1));
            Database.AddTimeTable(DateTime.Now.AddDays(10), DateTime.Now.AddHours(3), Database.GetTrainLine(2));
            Database.AddTimeTable(DateTime.Now.AddDays(3), DateTime.Now.AddHours(2), Database.GetTrainLine(3));

            Database.AddTimeTable(DateTime.Now, DateTime.Now.AddHours(3), Database.GetTrainLine(0));
            Database.AddTimeTable(DateTime.Now, DateTime.Now.AddHours(1), Database.GetTrainLine(1));
            Database.AddTimeTable(DateTime.Now, DateTime.Now.AddHours(5), Database.GetTrainLine(2));
            Database.AddTimeTable(DateTime.Now, DateTime.Now.AddHours(5), Database.GetTrainLine(3));
        }
    }
}
