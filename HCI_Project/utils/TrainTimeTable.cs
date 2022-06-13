using System;

namespace HCI_Project.utils
{
    public class TrainTimeTable
    {
        public int ID { get; private set; }
        public bool Deleted { get; set; }
        public DateTime DepartureDate { get; set; }
        public TrainLine TrainLine { get; private set; }
        public TimeTableConfiguration Configuration { get; private set; }

        public TrainTimeTable(int iD, DateTime departureDate, TimeTableConfiguration configuration)
        {
            ID = iD;
            Deleted = false;
            DepartureDate = departureDate;
            Configuration = configuration;
        }
        
        public bool TravelsBetween(string from, string to)
        {
            int stationFrom = Configuration.TrainLine.GetStationIndex(from);
            int stationTo = Configuration.TrainLine.GetStationIndex(to);
            return stationFrom != -1 && stationTo != -1 && stationFrom < stationTo;
        }
    }
}
