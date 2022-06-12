using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils.display
{
    public class TrainLineDisplay
    {
        public string Train { get; private set; }
        public int Capacity { get; private set; }
        public TimeSpan TravelTime { get; private set; }
        public string Stations { get; private set; }
        public TrainLine OriginalTrainLine { get; private set; }

        public TrainLineDisplay(TrainLine trainLine)
        {
            Train = $"{trainLine.Train.Name}.{trainLine.Train.ID}";
            Capacity = trainLine.Train.GetAllSeats();
            Stations = trainLine.stationsToString();
            TravelTime = trainLine.Stations.Last().Offset;
            OriginalTrainLine = trainLine;
        }
    }
}
