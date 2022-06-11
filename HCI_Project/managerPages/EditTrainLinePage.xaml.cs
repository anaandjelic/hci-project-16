using HCI_Project.popups;
using HCI_Project.utils;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HCI_Project.managerPages
{
    public partial class EditTrainLinePage : Page
    {
        private ObservableCollection<Station> Stations = new ObservableCollection<Station>();
        private readonly ObservableCollection<string> Trains;
        private List<Pushpin> Pins = new List<Pushpin>();
        private bool IsFirstStation = true;
        private Point StartPoint;
        public EditTrainLinePage()
        {
            InitializeComponent();
            Trains = new ObservableCollection<string>(Database.GetTrains().Select(t => $"{t.Name} - {t.FirstClassCapacity + t.SecondClassCapacity} seats").OrderBy(name => name).ToArray());
            TrainsCombobox.ItemsSource = Trains;
            TrainsCombobox.SelectedIndex = 0;
            DataContext = this;
        }

        private void MapView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartPoint = e.GetPosition(null);
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = StartPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                Image image = e.Source as Image;
                DataObject data = new DataObject(typeof(ImageSource), image.Source);
                DragDrop.DoDragDrop(image, data, DragDropEffects.Move);
            }
        }

        private void MapView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void MapView_Drop(object sender, DragEventArgs e)
        {
            string[] invalidFields = { "", "", "" };
            e.Handled = true;
            double lastPrice = Stations.Count == 0 ? 0 : Stations.Last().Price;
            TimeSpan lastTime = Stations.Count == 0 ? new TimeSpan() : Stations.Last().Offset;

            Point mousePosition = e.GetPosition(MyMap);
            Location pinLocation = MyMap.ViewportPointToLocation(mousePosition);

            NewStationDialog dialog = new NewStationDialog(IsFirstStation, lastPrice, lastTime,false,null);
            //vestacki postavljene vrednosti na false i null zasad!!!!

            bool? result = dialog.ShowDialog();
            if (!(bool)result)
            {
                return;
            }

            Pushpin pin = new Pushpin
            {
                Location = pinLocation,

                ToolTip = dialog.StationName,
                Height = 70,
                Width = 70,
                Template = (ControlTemplate)FindResource("CustomPushpinTemplate")
            };
            pin.MouseDoubleClick += new MouseButtonEventHandler(Pin_Click);

            Stations.Add(new Station(dialog.StationName, dialog.Price, pinLocation.Latitude, pinLocation.Longitude, dialog.Time));
            Pins.Add(pin);
            UpdateMap();

            if (IsFirstStation)
            {
                IsFirstStation = false;
            }
        }

        private void Pin_Click(object sender, RoutedEventArgs e)
        {
            Pushpin pin = (Pushpin)sender;
            Pins.Remove(pin);
            var station = (from s in Stations where s.Longitude == pin.Location.Longitude && s.Latitude == pin.Location.Latitude select s).FirstOrDefault();
            if (station.Offset.Equals(new TimeSpan(0, 0, 0)))
            {
                new MessageBoxCustom("You have to remove every other pin in order to remove the first one.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }
            Stations.Remove(station);
            UpdateMap();
            IsFirstStation = Stations.Count == 0;
        }

        private void CreateTrainLine(object sender, RoutedEventArgs e)
        {
            string selectedTrain = ((string)TrainsCombobox.SelectedItem).Split('-')[1].Trim();
            Train train = Database.GetTrain(Int32.Parse(selectedTrain));
            Database.AddTrainLine(Stations.ToList(), train);
            Stations = new ObservableCollection<Station>();
            Pins = new List<Pushpin>();
            MyMap.Children.Clear();
            IsFirstStation = true;
            StationGrid.ItemsSource = Stations;
        }

        private void DeleteTrainLine(object sender, RoutedEventArgs e)
        {
            var result = new MessageBoxCustom("You're about to delete this train line. This action is irreversible and will be applied to tickets and departures 5 days from now.\nDo you want to continue?", MessageType.Warning, MessageButtons.YesNo).ShowDialog();
            if ((bool)result)
            {
                Stations = new ObservableCollection<Station>();
                Pins = new List<Pushpin>();
                MyMap.Children.Clear();
                IsFirstStation = true;
                StationGrid.ItemsSource = Stations;
                //Database.DeleteTrainLine()
            }
        }

        private void ChangeTrainLine(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateMap()
        {
            MyMap.Children.Clear();
            List<BingMapsRESTToolkit.SimpleWaypoint> waypoints = new List<BingMapsRESTToolkit.SimpleWaypoint>();
            foreach (Station s in Stations)
                waypoints.Add(new BingMapsRESTToolkit.SimpleWaypoint(s.Latitude, s.Longitude));
            BingMapRESTServices.SendRequest(MyMap, waypoints);
            Pins.ForEach(x => MyMap.Children.Add(x));
            StationGrid.ItemsSource = Stations;
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
    }
}
