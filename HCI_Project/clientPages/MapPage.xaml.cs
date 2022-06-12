using HCI_Project.utils;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using ToastNotifications;
using ToastNotifications.Messages;
using System.Threading;
using ToastNotifications.Core;

namespace HCI_Project.clientPages
{
    public partial class MapPage : Page
    {
        Point StartPoint;
        private TrainLine trainLine;
        private Frame MainFrame;
        private bool isTutorialLine;
        private Notifier pageNotifier;
        private int brojac = 0;

        public MapPage(bool isTutorialLine,Frame MainFrame,Notifier notifier)
        {
            InitializeComponent();
            //BingMapRESTServices.SendRequest(MyMap);
            TrainLineSelect.ItemsSource = Database.GetTrainLinesStringWithID();


            //za tutorial
            this.isTutorialLine = isTutorialLine;
            this.MainFrame = MainFrame;
            this.pageNotifier = notifier;
            if (isTutorialLine)
            {
                string message = "Pritiskom na ComboBox dobijate ponudjene sve moguce Train linije \n Klikom na neku od njih Vam se iscrtava ta linija na mapi sa desne strane";
                //MessageBox.Show("Pritiskom na ComboBox dobijate ponudjene sve moguce Train linije \n Klikom na neku od njih Vam se iscrtava ta linija na mapi sa desne strane");
                notifications(message, "Information");
                this.SearchBar.IsEnabled = false;
                this.LB_result.IsEnabled = false;
                // zatim skacemo na combobox value changed jer kad izabere jednu od njih mozemo da nastavimo dalje sa tutorijalom
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

            //nastavak tutorijala 
            if (isTutorialLine)
            {
                // ovde enejblujemo search bar a disejblujemo combobox 
                this.TrainLineSelect.IsEnabled = false;
                //MessageBox.Show("Search bar Vam sluzi da detaljnije pretrazujete Train linije.\nU Search bar unosite naziv stanice/a odvojene razmakom.");
                
                string message = "Search bar Vam sluzi da detaljnije pretrazujete Train linije.\nU Search bar unosite naziv stanice/a odvojene razmakom.\n npr. belgrade";
                notifications(message, "Information");
                this.SearchBar.IsEnabled = true;
                //sad idemo skok na search bar i nakon unosa par slova izbacuemo poruku za enter i enejblujemo message box
            }

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
                if (isTutorialLine)
                {
                    //MessageBox.Show("Ovim se zavrsava tutorijal Search Train Lines, za ponovan prolazak pritisnite Ctrl U,T");
                    string message = "Ovim se zavrsava tutorijal Search Train Lines";
                    notifications(message, "Success");
                    this.isTutorialLine = false;
                    //return; // KAKO MOGU DA SE VRATIM NA POCETNI PROZOR!??!?!? JEDINO DA FORSIRAM LOGOUT??
                    //Thread.Sleep(2000);
                    MessageBox.Show("Klikom na ok dugme se vracate na login stranicu");
                    // zasad mora biti koriscen msgbox da bi se prikazala zeljena ruta
                    this.NavigationService.GoBack();
                }
            }
        }

        private void EnterIsPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (isTutorialLine)
                {
                    if (this.SearchBar.Text != "belgrade")
                        this.SearchBar.Text = "belgrade";

                    this.SearchBar.IsEnabled = false;
                    this.LB_result.IsEnabled = true;
                    //MessageBox.Show("Klikom na neku od opcija Vam se prikazuje selektovana linija na mapi pored");
                    string message = "Klikom na neku od opcija Vam se prikazuje selektovana linija na mapi pored";
                    notifications(message, "Information");
                }
                this.Button_Click(sender, e);
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isTutorialLine)
            {
                this.brojac++;
                //MessageBox.Show("Pritiskom na dugme ENTER u listboxu ispod search bara ce Vam se prikazati sve linije koje sadrze unetu stanicu");
                if (this.brojac % 8 == 0)
                {
                    string message = "Pritiskom na dugme ENTER u listboxu ispod search bara ce Vam se prikazati sve linije koje sadrze unetu stanicu";
                    notifications(message, "Information");
                    this.brojac = 0;
                }
            }
        }

        private void notifications(string message,string tip)
        {
            var optionsMax = new MessageOptions
            {
                FontSize = 30,
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true
            };
            if(tip=="Success")
                this.pageNotifier.ShowSuccess(message, optionsMax);
            else if(tip == "Information")
                this.pageNotifier.ShowInformation(message, optionsMax);
        }
    }
}
