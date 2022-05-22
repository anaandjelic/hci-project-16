using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace HCI_Project.LogInRegister
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private event Redirect redirect;
        public Registration(Redirect redirectEvent)
        {
            InitializeComponent();
            redirect = redirectEvent;
        }

        private void RegisterClick(object sender, System.Windows.RoutedEventArgs e)
        {
            redirect.Invoke();
        }
    }
   
}
