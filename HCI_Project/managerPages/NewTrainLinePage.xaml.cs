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
    public partial class NewTrainLinePage : Page
    {
        private ObservableCollection<Station> Stations = new ObservableCollection<Station>();
        private ObservableCollection<string> Trains;
        private bool IsFirstStation = true;
        private Point StartPoint;
        private double LastPrice;

        public NewTrainLinePage()
        {
            InitializeComponent();
            Trains = new ObservableCollection<string>(Database.GetTrains().Select(t => $"{t.Name} - {t.ID} - {t.Capacity}").OrderBy(name => name).ToArray());
            TrainsCombobox.ItemsSource = Trains;
            TrainsCombobox.SelectedIndex = 0;
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
            e.Handled = true;

            Point mousePosition = e.GetPosition(MyMap);
            Location pinLocation = MyMap.ViewportPointToLocation(mousePosition);

            NewStationDialog dialog = new NewStationDialog(IsFirstStation);

            dialog.ShowDialog();

            Pushpin pin = new Pushpin
            {
                Location = pinLocation,

                ToolTip = dialog.GetName(),
                Height = 70,
                Width = 70,
                Template = (ControlTemplate)FindResource("CustomPushpinTemplate")
            };


            Stations.Add(new Station(dialog.GetName(), dialog.GetPrice(), pinLocation.Latitude, pinLocation.Longitude, dialog.GetTime()));
            StationGrid.ItemsSource = Stations;

            if (!IsFirstStation)
                DrawLine();
            IsFirstStation = false;
            MyMap.Children.Add(pin);
        }

        private void CreateTrainLine(object sender, RoutedEventArgs e)
        {
            string selectedTrain = ((string)TrainsCombobox.SelectedItem).Split('-')[1].Trim();
            Train train = Database.GetTrain(Int32.Parse(selectedTrain));
            Database.AddTrainLine(Stations.ToList(), train);
        }

        private void DrawLine()
        {
            List<double[]> locations = new List<double[]>();
            foreach (Station s in Stations)
                locations.Add(new double[] { s.Latitude, s.Longitude });
            BingMapRESTServices.SendRequest(MyMap, locations);
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
