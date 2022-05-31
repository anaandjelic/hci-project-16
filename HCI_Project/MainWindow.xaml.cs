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
            Database.AddTrainLine(new List<string> { "Kljajicevo", "Sivac", "Vrbas" }, "Sombor", "Novi Sad", 1000.0, Database.GetTrain(0));
            Database.AddTimeTable(DateTime.Now, DateTime.Now.AddHours(2), Database.GetTrainLine(0));
        }
    }
}
