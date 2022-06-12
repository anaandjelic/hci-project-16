using HCI_Project.utils;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using HCI_Project.utils.display;
using System;
using System.Collections.Generic;

namespace HCI_Project.clientPages
{
    public partial class SearchPage : Page
    {
        readonly ObservableCollection<TrainTimeTable> ttbs = new ObservableCollection<TrainTimeTable>(Database.GetTimeTables());
        public SearchPage()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));

            DataTable.ItemsSource = new ObservableCollection<TTT_DTO>(Database.GetTTT_DTOS());
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
    }
}
