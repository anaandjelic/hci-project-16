using System.Windows.Controls;

namespace HCI_Project.LogInRegister
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        private event Redirect redirect;
        public LogIn(Redirect redirectEvent)
        {
            InitializeComponent();
            redirect = redirectEvent;
        }

        private void LogInClick(object sender, System.Windows.RoutedEventArgs e)
        {
            redirect.Invoke();
        }
    }
}
