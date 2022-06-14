using HCI_Project.utils;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using HCI_Project.popups;
using HCI_Project.utils.display;

namespace HCI_Project.managerPages
{
    public partial class TrainLineSearch : Page
    {
        public ObservableCollection<TrainLineDisplay> TrainLines;
        private TrainLine SelectedTrainLine;

        public TrainLineSearch()
        {
            InitializeComponent();
            DataContext = this;
            TrainLines = new ObservableCollection<TrainLineDisplay>(Database.GetTrainLineDisplays());
            TrainLineGrid.ItemsSource = TrainLines;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyMap.Children.Clear();
            DrawLine();
        }

        private void DrawLine()
        {
            try
            {
                SelectedTrainLine = (TrainLineGrid.SelectedItem as TrainLineDisplay).OriginalTrainLine;
                List<BingMapsRESTToolkit.SimpleWaypoint> waypoints = new List<BingMapsRESTToolkit.SimpleWaypoint>();
                foreach (Station s in SelectedTrainLine.Stations)
                    waypoints.Add(new BingMapsRESTToolkit.SimpleWaypoint(s.Latitude, s.Longitude));
                BingMapRESTServices.SendRequest(MyMap, waypoints);
            }
            catch (Exception e)
            {
                new MessageBoxCustom("An error has occured.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private void SearchTrainLines(object sender, RoutedEventArgs e)
        {
            string enteredValues = SearchTextBox.Text.ToString().Trim().ToLower();
            string[] stationsNames = enteredValues.Split();
            TrainLines = new ObservableCollection<TrainLineDisplay>(Database.SearchConfigured(stationsNames));
            TrainLineGrid.ItemsSource = TrainLines;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.TimeSpan))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "hh\\:mm";
            switch (e.Column.Header)
            {
                case "TravelTime":
                    e.Column.Header = "Travel time";
                    break;
                case "OriginalTrainLine":
                    e.Column.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void EditTrainLine(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedTrainLine = (TrainLineGrid.SelectedItem as TrainLineDisplay).OriginalTrainLine;
                NavigationService?.Navigate(new EditTrainLinePage(SelectedTrainLine));
            }
            catch (Exception ex)
            {
                new MessageBoxCustom("You have to select a train line.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}
