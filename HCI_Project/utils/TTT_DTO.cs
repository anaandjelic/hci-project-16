using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils
{
    public class TTT_DTO
    {
        //public int ID { get; private set; }
        public DateTime Departure { get; private set; }
        public DateTime Arrival { get; private set; }
        public string Line { get; private set; }
        public TimeSpan Time { get; private set; }

        public TTT_DTO(DateTime departureTime, DateTime arrivalTime, String trainLine)
        {
            //ID = iD;
            Departure = departureTime;
            Arrival = arrivalTime;
            Line = trainLine;
            Time = arrivalTime - departureTime;
        }
        public TTT_DTO(TrainTimeTable trainTimeTable)
        {
            //ID = trainTimeTable.ID;
            Departure = trainTimeTable.DepartureDate + trainTimeTable.Configuration.DepartureTime;
            Arrival = Departure + trainTimeTable.TrainLine.Stations.Last().Offset;
            Time = Arrival - Departure;
            Line = trainTimeTable.TrainLine.stationsToString();
        }
    }
}
