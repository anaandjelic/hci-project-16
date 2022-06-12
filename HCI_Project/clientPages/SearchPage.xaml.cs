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
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));

            DataTable.ItemsSource = new ObservableCollection<TimeTableDisplay>();
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            string from = From.Text;
            string to = To.Text;
            DateTime date = DP.SelectedDate ?? DateTime.Now.Date;
            List<TimeTableDisplay> searchRes = Database.GetTimeTableDisplays(from, to, date);

            DataTable.ItemsSource = searchRes;
            DataTable.Items.Refresh();
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
            }
        }
    }
}
