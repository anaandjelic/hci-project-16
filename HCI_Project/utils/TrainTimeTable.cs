using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils
{
    public class TrainTimeTable
    {
        public int ID { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public TrainLine TrainLine { get; private set; }
        public TimeSpan TravelTime { get; private set; }

        public TrainTimeTable(int iD, DateTime departureTime, DateTime arrivalTime, TrainLine trainLine)
        {
            ID = iD;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            TrainLine = trainLine;
            TravelTime = arrivalTime - departureTime;
        }
    }
}
