using System.Collections.Generic;
using System.Linq;

namespace HCI_Project.utils
{
    public class TrainLine
    {
        public int ID { get; private set; }
        public bool Deleted { get; set; }
        public List<Station> Stations { get; private set; }
        public Train Train { get; set; }


        public TrainLine(int id, List<Station> stations, Train train)
        {
            ID = id;
            Deleted = false;
            Stations = stations;
            Train = train;
        }

        public int GetStationIndex(string name)
        {
            Station station = Stations.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            return station == null ? -1 : Stations.IndexOf(station);
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

        //dodato
        public string stationsToStringWithID()
        {
            string res = ID.ToString() + " -> ";
            foreach (Station station in Stations)
            {
                res += station.Name;
                res += " - ";
            }
            return res.Substring(0, res.Length - 3);
        }
        //dodato
        public Station getStationByName(string sname)
        {
            foreach (Station station in Stations)
            {
                if (station.Name.ToLower() == sname.ToLower())
                    return station;
            }
            return null;
        }
        public override bool Equals(object obj)
        {
            return obj is TrainLine line &&
                   ID == line.ID;
        }

        public override int GetHashCode()
        {
            return 1213502048 + ID.GetHashCode();
        }
    }
}
