using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils
{
    public class TrainLine
    {
        public int ID { get; private set; }
        public List<string> Stops { get; private set; }
        public string Departure { get; private set; }
        public string Destination { get; private set; }
        public double Price { get; private set; }
        public Train Train { get; private set; }

        public TrainLine(int iD, List<string> stops, string departure, string destination, double price, Train train)
        {
            ID = iD;
            Stops = stops;
            Departure = departure;
            Destination = destination;
            Price = price;
            Train = train;
        }
        public void AddStop(string stop)
        {
            Stops.Add(stop);
        }
        public void RemoveStop(string stop)
        {
            Stops.Remove(stop);
        }
    }
}
