using HCI_Project.utils;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

namespace HCI_Project.clientPages
{
    public partial class MapPage : Page
    {
        Point StartPoint;
        private TrainLine trainLine;

        public MapPage()
        {
            InitializeComponent();
            //BingMapRESTServices.SendRequest(MyMap);

            TrainLineSelect.ItemsSource = Database.GetTrainLinesStringWithID();
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

            Pushpin pin = new Pushpin
            {
                Location = pinLocation,

                ToolTip = "This is a pushpin with custom image",
                Height = 70,
                Width = 70,
                Template = (ControlTemplate)FindResource("CustomPushpinTemplate")
            };

            //Coordinates.Text = pinLocation.Longitude.ToString();
            //Coordinates1.Text = pinLocation.Latitude.ToString();
            MyMap.Children.Add(pin);
        }

        private int getTrainLineID()
        {
            string trainLineString = TrainLineSelect.SelectedValue.ToString();
            string trainLineID_String = trainLineString.Split('>')[0];
            trainLineID_String = trainLineID_String.Substring(0, trainLineID_String.Length - 2);
            int trainLineID = Convert.ToInt32(trainLineID_String.Trim());
            return trainLineID;
        }

        private void TrainLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyMap.Children.Clear();
            int trainLineID = getTrainLineID();
            trainLine = Database.GetTrainLineByID(trainLineID);
            DrawLine();
        }

        private void DrawLine()
        {
            List<BingMapsRESTToolkit.SimpleWaypoint> waypoints = new List<BingMapsRESTToolkit.SimpleWaypoint>();
            foreach (Station s in trainLine.Stations)
                waypoints.Add(new BingMapsRESTToolkit.SimpleWaypoint(s.Latitude, s.Longitude));
            BingMapRESTServices.SendRequest(MyMap, waypoints);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LB_result.Items.Clear();
            string enteredValues = SearchBar.Text.ToString().Trim().ToLower();
            string[] stationsNames = enteredValues.Split();
            foreach (string value in Database.findLinesWithStations(stationsNames))
                LB_result.Items.Add(value.ToUpper());
        }

        private int getTrainLineID_forLB()
        {
            if (LB_result.SelectedItem!=null)
            {
                string trainLineString = LB_result.SelectedItem.ToString();
                string trainLineID_String = trainLineString.Split('>')[0];
                trainLineID_String = trainLineID_String.Substring(0, trainLineID_String.Length - 2);
                int trainLineID = Convert.ToInt32(trainLineID_String.Trim());
                return trainLineID;
            }
            return -1;
        }

        private void LB_result_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyMap.Children.Clear();
            int trainLineID = getTrainLineID_forLB();
            if (trainLineID != -1)
            {
                trainLine = Database.GetTrainLineByID(trainLineID);
                DrawLine();
            }
        }

        private void EnterIsPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                this.Button_Click(sender,e);
        }
    }
}
