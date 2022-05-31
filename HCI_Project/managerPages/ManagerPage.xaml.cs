﻿using HCI_Project.LogInRegister;
using HCI_Project.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
