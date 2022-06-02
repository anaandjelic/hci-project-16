using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HCI_Project.utils
{
    public class Station
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public TimeSpan Offset { get; private set; }

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
