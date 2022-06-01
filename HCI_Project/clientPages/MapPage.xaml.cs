using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HCI_Project.utils;
using Microsoft.Maps.MapControl.WPF;

namespace HCI_Project.clientPages
{
    public partial class MapPage : Page
    {
        Point StartPoint;

        public MapPage()
        {
            InitializeComponent();
            BingMapRESTServices.SendRequest(MyMap);
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
    }
}
