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
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Messages;

namespace HCI_Project.managerPages
{
    public partial class NewTrainLinePage : Page
    {
        private ObservableCollection<Station> Stations = new ObservableCollection<Station>();
        private readonly ObservableCollection<Train> Trains;
        private List<Pushpin> Pins = new List<Pushpin>();
        private bool IsFirstStation = true;
        private Point StartPoint;

        private bool isTutorialCreateTrainLine;
        private Frame MainFrame;
        private Notifier managerNotifier;


        public NewTrainLinePage(bool isTutorialCreateTrainLine, Frame MainFrame, Notifier notifier)
        {
            InitializeComponent();
            Trains = new ObservableCollection<Train>(Database.GetTrains());
            TrainsCombobox.ItemsSource = Trains;
            TrainsCombobox.SelectedIndex = 0;
            DataContext = this;

            this.isTutorialCreateTrainLine = isTutorialCreateTrainLine;
            this.MainFrame = MainFrame;
            this.managerNotifier = notifier;

            if (isTutorialCreateTrainLine)
            {
                this.MyMap.IsEnabled = false;
                this.CreateBtn.IsEnabled = false;
                this.cancelBTN.IsEnabled = false;
                //string message = "Klikom na combobox dobijate mogucnost odabira voza za vasu rutu";
                string message = "By clicking on the combobox you will be given a choice ov your desired route";
                this.TrainsCombobox.Focus();
                notifications(message, "Information");
            }

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

            NewStationDialog dialog = new NewStationDialog(IsFirstStation, lastPrice, lastTime,isTutorialCreateTrainLine,this.managerNotifier);

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

            if (this.isTutorialCreateTrainLine && this.Stations.Count() == 1)
            {
                //string message = "Pin mozete da kliknete i prevucete na mapu";
                string message = "You can click and drag the pin on top of the map";
                notifications(message, "Information");
            }
            else if (this.isTutorialCreateTrainLine && this.Stations.Count()==2)
            {
                MyMap.IsEnabled = false;
                string message = "By clicking on the Confirm button you will create a new train line";
                //string message = "Klikom na Confirm dugme kreirate novi train line";
                notifications(message, "Information");
                this.CreateBtn.IsEnabled = true;
            }

            if (IsFirstStation)
            {
                IsFirstStation = false;
            }
        }

        private void Pin_Click(object sender, RoutedEventArgs e)
        {
            Pushpin pin = sender as Pushpin;
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
            try
            {
                Train train = TrainsCombobox.SelectedItem as Train;
                Database.AddTrainLine(Stations.ToList(), train);
                Stations = new ObservableCollection<Station>();
                Pins = new List<Pushpin>();
                MyMap.Children.Clear();
                IsFirstStation = true;
                StationGrid.ItemsSource = Stations;
                new MessageBoxCustom("You have successfully created a train line.", MessageType.Success, MessageButtons.Ok).ShowDialog();

                if (this.isTutorialCreateTrainLine)
                {
                    //string message = "Ovim se zavrsava tutorija za kreiranje train linova";
                    string message = "This ends the Create Train Line Tutorial";
                    notifications(message, "Success");
                    //MessageBox.Show("Klikom na ok se vracate na login page");
                    new MessageBoxCustom("By clicking the ok button you will be returned to the last page you were on", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    this.NavigationService.GoBack();
                }
                ClearForm();
            }
            catch (Exception ex)
            {
                new MessageBoxCustom("There has been an error.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private void CancelTrainLine(object sender, RoutedEventArgs e)
        {
            var result = new MessageBoxCustom("You're about to remove all progress made on this page. This action is irreversible.\nDo you want to continue?", MessageType.Warning, MessageButtons.YesNo).ShowDialog();
            if ((bool)result)
            {
                ClearForm();
            }
        }

        private void ClearForm()
        {
            Stations = new ObservableCollection<Station>();
            Pins = new List<Pushpin>();
            MyMap.Children.Clear();
            IsFirstStation = true;
            StationGrid.ItemsSource = Stations;
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

        private void notifications(string message, string tip)
        {
            var optionsMax = new MessageOptions
            {
                FontSize = 30,
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true
            };
            if (tip == "Success")
                this.managerNotifier.ShowSuccess(message, optionsMax);
            else if (tip == "Information")
                this.managerNotifier.ShowInformation(message, optionsMax);
        }

        private void TrainsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.isTutorialCreateTrainLine)
            {
                this.TrainsCombobox.IsEnabled = false;
                this.MyMap.IsEnabled = true;
                //string message = "Pin mozete da kliknete i prevucete na mapu";
                string message = "You can click and drag and drop the Pin on top of the map";
                notifications(message, "Information");
                //message = "Zatim pustanjem pina dobijate stanicu";
                message = "By dropping the pin on the map you will be prompted by a station creation window";
                notifications(message, "Information");
            }
        }
    }
}
