using HCI_Project.LogInRegister;
using HCI_Project.managerPages;
using System;
using System.Windows;
using System.Windows.Data;

namespace HCI_Project
{
    public delegate void Redirect();
    public partial class MainWindow : Window
    {
        private event Redirect RedirectToManagerEvent;

        public MainWindow()
        {
            InitializeComponent();
            RedirectToManagerEvent += Register;
            MainFrame.Content = new LogInRegisterPage(RedirectToManagerEvent);
        }

        private void Register()
        {
            MainFrame.Content = new ManagerPage();
        }
    }
}
