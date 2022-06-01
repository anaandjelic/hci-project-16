using HCI_Project.LogInRegister;
using HCI_Project.utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace HCI_Project
{
    public delegate void Redirect();
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new LogInPage(MainFrame);
            Database.AddUser("pera", "pera", "Pera", "Peric", UserRole.CLIENT);
            Database.AddUser("maja", "maja", "Maja", "Majic", UserRole.MANAGER);
            Database.AddTrain(5);
            Database.AddTrain(2);
            List<Station> stations1 = new List<Station>()
            {
                new Station("Sombor", 45.773979, 19.118759, new TimeSpan(0)),
                new Station("Vrbas", 45.572021, 19.639780, new TimeSpan(1, 15, 0)),
                new Station("Novi Sad", 45.254410, 19.842550, new TimeSpan(2, 0, 0)),
                new Station("Belgrade", 44.794043, 20.461006, new TimeSpan(3, 20, 0))
            };
            List<Station> stations2 = new List<Station>()
            {
                new Station("Subotica", 46.094150, 19.66, new TimeSpan(0)),
                new Station("Novi Sad", 45.254410, 19.842550, new TimeSpan(1, 25, 0)),
                new Station("Belgrade", 44.794043, 20.461006, new TimeSpan(2, 10, 0)),
                new Station("Valjevo", 44.273660, 19.884121, new TimeSpan(3, 30, 0))
            };
            Database.AddTrainLine(stations1, 1000.0, Database.GetTrain(0));
            Database.AddTimeTable(DateTime.Now, DateTime.Now.AddHours(2), Database.GetTrainLine(0));
        }
    }
}
