using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils
{
    public class Station
    {
        public string Name { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public TimeSpan Offset { get; private set; }

        public Station(string name, double latitude, double longitude, TimeSpan offset)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Offset = offset;
        }
    }
}
