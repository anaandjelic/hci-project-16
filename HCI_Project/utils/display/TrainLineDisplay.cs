using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils.display
{
    class TrainLineDisplay
    {
        public string Train { get; private set; }
        public string Stations { get; private set; }
        public TimeSpan TravelTime { get; private set; }

        public TrainLineDisplay(TrainLine trainLine)
        {
            Train = $"{trainLine.Train.Name}.{trainLine.Train.ID}";
            Stations = trainLine.stationsToString();
            TravelTime = trainLine.Stations.Last().Offset;
        }
    }
}
