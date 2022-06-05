using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils
{
    public class TTT_DTO
    {
        public int ID { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public string TrainLine { get; private set; }
        public TimeSpan TravelTime { get; private set; }

        public TTT_DTO(int iD, DateTime departureTime, DateTime arrivalTime, String trainLine)
        {
            ID = iD;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            TrainLine = trainLine;
            TravelTime = arrivalTime - departureTime;
        }
        public TTT_DTO(TrainTimeTable trainTimeTable)
        {
            ID = trainTimeTable.ID;
            DepartureTime = trainTimeTable.DepartureTime;
            ArrivalTime = trainTimeTable.ArrivalTime;
            TravelTime = trainTimeTable.TravelTime;
            TrainLine = trainTimeTable.TrainLine.stationsToString();
        }
    }
}
