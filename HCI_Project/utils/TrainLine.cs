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
        public List<Station> Stations { get; private set; }
        public double Price { get; private set; }
        public Train Train { get; private set; }

        public TrainLine(int iD, List<Station> stations, double price, Train train)
        {
            ID = iD;
            Stations = stations;
            Price = price;
            Train = train;
        }
        public void AddStation(Station station, int position)
        {
            Stations.Insert(position, station);
        }
        public void RemoveStop(Station station)
        {
            Station res = (Station)(from s in Stations where s.Name == station.Name select s).FirstOrDefault();
            Stations.Remove(res);
        }
        public void RemoveStop(int index)
        {
            Stations.RemoveAt(index);
        }
    }
}
