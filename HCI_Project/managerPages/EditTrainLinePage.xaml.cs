using HCI_Project.popups;
using HCI_Project.utils;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HCI_Project.managerPages
{
    public partial class EditTrainLinePage : Page
    {
        private readonly ObservableCollection<Train> Trains;
        private readonly ObservableCollection<Station> Stations = new ObservableCollection<Station>();
        private TrainLine OriginalTrainLine;

        public Station CurrentItem;

        public EditTrainLinePage(TrainLine trainLine)
        {
            try
            {
                OriginalTrainLine = trainLine;
                InitializeComponent();
                Trains = new ObservableCollection<Train>(Database.GetTrains());
                TrainsCombobox.ItemsSource = Trains;
                StationGrid.ItemsSource = Stations;
                TrainsCombobox.SelectedIndex = 0;
                DataContext = this;

                List<BingMapsRESTToolkit.SimpleWaypoint> waypoints = new List<BingMapsRESTToolkit.SimpleWaypoint>();
                foreach (Station s in trainLine.Stations)
                    waypoints.Add(new BingMapsRESTToolkit.SimpleWaypoint(s.Latitude, s.Longitude));
                BingMapRESTServices.SendRequest(MyMap, waypoints);

                foreach (var station in trainLine.Stations)
                {
                    Pushpin pin = new Pushpin
                    {
                        Location = new Location()
                        {
                            Latitude = station.Latitude,
                            Longitude = station.Longitude
                        },

                        ToolTip = station.Name,
                        Height = 70,
                        Width = 70,
                        Template = (ControlTemplate)FindResource("CustomPushpinTemplate")
                    };
                    MyMap.Children.Add(pin);
                    Stations.Add(station);
                }
            }
            catch (Exception e)
            {
                new MessageBoxCustom("An error has occured.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }


        private void DeleteTrainLine(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = new MessageBoxCustom("You're about to delete this train line. This action is irreversible and will be applied to tickets and departures 5 days from now.Do you want to continue?", MessageType.Warning, MessageButtons.YesNo).ShowDialog();
                if ((bool)result)
                {
                    Database.DeleteTrainLine(OriginalTrainLine);
                }
                new MessageBoxCustom("You have successfully deleted this train line", MessageType.Success, MessageButtons.Ok).ShowDialog();
                NavigationService?.Navigate(new TrainLineSearch());
            }
            catch (Exception ex)
            {
                new MessageBoxCustom("There has been an error", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }


        private void StationGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Name")
            {
                e.Column.Header = "Name";
            }
            if (e.Column.Header.ToString() == "Latitude")
            {
                e.Column.Visibility = Visibility.Hidden;
            }
            if (e.Column.Header.ToString() == "Longitude")
            {
                e.Column.Visibility = Visibility.Hidden;
            }
            if (e.Column.Header.ToString() == "Offset")
            {
                e.Column.Header = "Time";
            }
            if (e.Column.Header.ToString() == "Price")
            {
                e.Column.Header = "Price";
            }
        }

        private void EditBeginning(object sender, DataGridBeginningEditEventArgs e)
        {
            TextBlock tb = (TextBlock)e.Column.GetCellContent(e.Row);
            Station item = (Station)tb.DataContext;
            CurrentItem = new Station(item.Name, item.Price, 0, 0, item.Offset);
        }

        private void EditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            string columnName = e.Column.Header.ToString();
            int index = e.Row.GetIndex();
            var element = e.EditingElement as TextBox;
            if (index == 0)
            {
                if (columnName == "Name")
                    element.Text = CurrentItem.Name;
                else if (columnName == "Time")
                    element.Text = CurrentItem.Offset.ToString();
                else if (columnName == "Price")
                    element.Text = CurrentItem.Price.ToString();
                return;
            }
            string s = (((TextBox)e.EditingElement).Text);
            if (columnName == "Name")
            {
                if (NameIsValid(s))
                {
                    Stations.ElementAt(index).Name = s;
                }
                else
                {
                    element.Text = CurrentItem.Name;
                }
            }
            else if (columnName == "Time")
            {
                if (TimeIsValid(s, index))
                {
                    Stations.ElementAt(index).Offset = TimeSpan.Parse(s);
                }
                else
                {
                    element.Text = CurrentItem.Offset.ToString();
                }
            }
            else if (columnName == "Price")
            {
                if (PriceIsValid(s, index))
                {
                    Stations.ElementAt(index).Price = double.Parse(s);
                }
                else
                {
                    element.Text = CurrentItem.Price.ToString();
                }
            }
        }

        private bool NameIsValid(string s)
        {
            return !string.IsNullOrWhiteSpace((s ?? "").ToString()) && Regex.IsMatch(s, @"\A[\p{L}\s]+\Z");
        }

        private bool TimeIsValid(string s, int index)
        {
            var time = TimeSpan.Parse(s);
            if (index == 0) return false;
            var previous = index == 0 ? null : Stations.ElementAt(index - 1);
            var next = index == Stations.Count - 1 ? null : Stations.ElementAt(index + 1);

            if (index == Stations.Count - 1) return !string.IsNullOrWhiteSpace((s ?? "").ToString()) && time > previous.Offset;

            if (!string.IsNullOrWhiteSpace((s ?? "").ToString()) && next.Offset > time && time > previous.Offset)
                return true;
            return false;
        }

        private bool PriceIsValid(string s, int index)
        {
            if (index == 0) return false;
            var previous = index == 0 ? null : Stations.ElementAt(index - 1);
            var next = index == Stations.Count - 1 ? null : Stations.ElementAt(index + 1);

            if (index == Stations.Count - 1) return !string.IsNullOrWhiteSpace((s ?? "").ToString()) && double.TryParse((string)s, out _) && double.Parse(s) > previous.Price;

            if (!string.IsNullOrWhiteSpace((s ?? "").ToString()) && double.TryParse((string)s, out _)
                && next.Price > double.Parse(s.ToString()) && double.Parse(s.ToString()) > previous.Price)
                return true;
            return false;
        }

        private void TrainChanged(object sender, SelectionChangedEventArgs e)
        {
            OriginalTrainLine.Train = (sender as ComboBox).SelectedItem as Train;
        }
    }
}
