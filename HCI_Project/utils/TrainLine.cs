using System.Collections.Generic;
using System.Linq;

namespace HCI_Project.utils
{
    public class TrainLine
    {
        public int ID { get; private set; }
        public List<Station> Stations { get; private set; }
        public Train Train { get; private set; }


        public TrainLine(int iD, List<Station> stations, Train train)
        {
            ID = iD;
            Stations = stations;
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


        //dodato
        public List<string> GetStationsNames()
        {
            List<string> snames = new List<string>();
            foreach (Station station in Stations)
                snames.Add(station.Name);
            return snames;
        }

        //dodato
        public bool DoesItContainStationName(string sname)
        {
            if (sname.Equals(""))
                return true;
            List<string> stationNames = GetStationsNames();
            foreach (string stationName in stationNames)
                if (stationName.Equals(sname))
                    return true;
            return false;
        }

        //dodato
        public string stationsToString()
        {
            string res = "";
            foreach (Station station in Stations)
            {
                res+=station.Name;
                res+=" - ";
            }
            return res.Substring(0,res.Length-3);
        }

    }
}
