using System.Windows;
using System.Windows.Forms;

namespace HCI_Project.help
{
    public partial class HelpViewer : Window
    {
        private string HelpName;
        public HelpViewer(string helpName)
        {
            HelpName = helpName;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var path = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "help", HelpName);
            browser.Navigate(path);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (browser.CanGoBack)
            {
                browser.GoBack();
            }
        }

        private void Forward(object sender, RoutedEventArgs e)
        {
            if (browser.CanGoForward)
            {
                browser.GoForward();
            }
        }
    }
}
