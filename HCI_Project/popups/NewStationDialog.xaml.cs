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
using System.Windows.Shapes;

namespace HCI_Project.popups
{
    public partial class NewStationDialog : Window
    {
        public NewStationDialog(bool isFirstStation)
        {
            InitializeComponent();
            if (isFirstStation)
            {
                Time.IsEnabled = false;
                Price.IsEnabled = false;
            }
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Name.SelectAll();
            Name.Focus();
        }

        public string GetName()
        {
            return Name.Text;
        }
        public double GetPrice()
        {
            return Price.IsEnabled ? double.Parse(Price.Text) : 0;
        }
        public TimeSpan GetTime()
        {
            return Time.IsEnabled ? new TimeSpan(Time.SelectedTime.Value.Hour, Time.SelectedTime.Value.Minute, 0) : new TimeSpan(0, 0, 0);
        }
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            e.Handled = !double.TryParse(fullText, out _);
        }
    }
}
