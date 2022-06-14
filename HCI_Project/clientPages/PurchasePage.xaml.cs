using HCI_Project.utils;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using HCI_Project.utils.display;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HCI_Project.clientPages
{
    public partial class PurchasePage : Page
    {
        private Frame ClientFrame;

        public PurchasePage(Frame clientFrame)
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            ClientFrame = clientFrame;

            AvailableTimesTable.ItemsSource = new ObservableCollection<TimeTableDisplay>();
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            string from = From.Text;
            string to = To.Text;
            DateTime date = DP.SelectedDate ?? DateTime.Now.Date;

            if (date < DateTime.Now.Date)
            {
                MessageBox.Show("Please choose a date that is not in the past.");
                return;
            }

            List<TimeTableDisplay> searchRes = Database.GetTimeTableDisplays(from, to, date);

            AvailableTimesTable.ItemsSource = searchRes;
            AvailableTimesTable.Items.Refresh();
        }

        private void ContinueClick(object sender, RoutedEventArgs e)
        {
            if (AvailableTimesTable.SelectedItem == null)
            {
                MessageBox.Show("You must select the time before proceeding.");
                return;
            }

            TimeTableDisplay selectedTime = (TimeTableDisplay)AvailableTimesTable.SelectedItem;
            ClientFrame.Content = new PickSeatPage(selectedTime, From.Text, To.Text, ClientFrame);
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "hh:mm";
            switch (e.Column.Header)
            {
                case "TravelTime":
                    e.Column.Header = "Travel time";
                    break;
                case "AvailableSeats":
                    e.Column.Header = "Available seats";
                    break;
                case "OriginalTimeTable":
                    e.Column.Visibility = Visibility.Hidden;
                    break;
            }
        }
    }
}
