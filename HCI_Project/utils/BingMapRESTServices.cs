using BingMapsRESTToolkit;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;

namespace HCI_Project.utils
{
    public static class BingMapRESTServices
    {
        public static void SendRequest(Map myMap)
        {
            RouteRequest routeRequest = new RouteRequest()
            {
                RouteOptions = new RouteOptions()
                {
                    TravelMode = TravelModeType.Driving,
                    RouteAttributes = new List<RouteAttributeType>() 
                    { 
                        RouteAttributeType.RoutePath,
                        RouteAttributeType.ExcludeItinerary
                    }
                },
                Waypoints = new List<SimpleWaypoint>() {
                    new SimpleWaypoint("Subotica, RS"),
                    new SimpleWaypoint("Belgrade, RS")
                },
                BingMapsKey = "jcN70CoOBDnsrAlWKqWM~KGgDd7HLbAy2iRafyB9ytw~ApXB3OvpKS1Loo6XUCGodM0rf6U6ph0Jtc-kt3pk3FWMZ8zZVBhOxxMSViza80RG"
            };

            ProcessRequest(routeRequest, myMap);

        }

        private static async void ProcessRequest(BaseRestRequest request, Map myMap)
        {
            try
            {
                //Execute the request.
                Response response = await request.Execute((remainingTime) => { });

                Route route = response.ResourceSets[0].Resources[0] as Route;

                double[][] routePath = route.RoutePath.Line.Coordinates;
                LocationCollection locs = new LocationCollection();

                for (int i = 0; i < routePath.Length; i++)
                {
                    if (routePath[i].Length >= 2)
                    {
                        locs.Add(new Microsoft.Maps.MapControl.WPF.Location(routePath[i][0], routePath[i][1]));
                    }
                }

                MapPolyline routeLine = new MapPolyline()
                {
                    Locations = locs,
                    Stroke = new SolidColorBrush(Colors.Blue),
                    StrokeThickness = 2
                };

                myMap.Children.Add(routeLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
