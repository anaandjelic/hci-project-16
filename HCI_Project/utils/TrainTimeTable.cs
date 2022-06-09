using System;

namespace HCI_Project.utils
{
    public class TrainTimeTable
    {
        public int ID { get; private set; }
        public bool Deleted { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; private set; }
        public TrainLine TrainLine { get; private set; }
        public TimeSpan TravelTime { get; private set; }
        public TimeTableConfiguration Configuration { get; private set; }

        public TrainTimeTable(int iD, DateTime departureTime, DateTime arrivalTime, TrainLine trainLine, TimeTableConfiguration configuration)
        {
            ID = iD;
            Deleted = false;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            TrainLine = trainLine;
            TravelTime = arrivalTime - departureTime;
            Configuration = configuration;
        }
    }
}
