using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HCI_Project.utils
{
    public class Station
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public TimeSpan Offset { get; set; }

        public Station() { }
        public Station(string name, double price, double latitude, double longitude, TimeSpan offset)
        {
            Name = name;
            Price = price;
            Latitude = latitude;
            Longitude = longitude;
            Offset = offset;
        }
    }
}
