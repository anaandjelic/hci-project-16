using HCI_Project.utils;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project.clientPages
{
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));

            DataTable.ItemsSource = new ObservableCollection<TrainTimeTable>(Database.GetTimeTables());
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
