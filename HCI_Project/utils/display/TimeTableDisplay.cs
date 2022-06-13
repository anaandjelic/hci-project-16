using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils.display
{
    public class TimeTableDisplay
    {
        public string Train { get; private set; }
        public DateTime Departure { get; private set; }
        public DateTime Arrival { get; private set; }
        public TimeSpan TravelTime { get; private set; }
        public double Price { get; private set; }
        public int AvailableSeats { get; private set; }
        public TimeTableDisplay(TrainTimeTable timeTable, string from, string to, int availableSeats)
        {
            Train = $"{timeTable.Configuration.TrainLine.Train.Name}.{timeTable.Configuration.TrainLine.Train.ID}";
            Departure = timeTable.DepartureDate.Date + timeTable.Configuration.DepartureTime + timeTable.Configuration.TrainLine.getStationByName(from).Offset;
            Arrival = timeTable.DepartureDate.Date + timeTable.Configuration.DepartureTime + timeTable.Configuration.TrainLine.getStationByName(to).Offset;
            TravelTime = Arrival - Departure;
            Price = timeTable.Configuration.TrainLine.getStationByName(to).Price - timeTable.Configuration.TrainLine.getStationByName(from).Price;
            AvailableSeats = availableSeats > -1 ? availableSeats : 0;
        }
    }
}
