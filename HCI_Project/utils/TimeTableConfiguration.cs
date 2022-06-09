using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils
{
    public class TimeTableConfiguration
    {
        public int ID { get; private set; }
        public TrainLine TrainLine { get; private set; }
        public TimeSpan DepartureTime { get; private set; }
        public bool Monday { get; private set; }
        public bool Tuesday { get; private set; }
        public bool Wednesday { get; private set; }
        public bool Thursday { get; private set; }
        public bool Friday { get; private set; }
        public bool Saturday { get; private set; }
        public bool Sunday { get; private set; }
        public bool Deleted { get; set; }

        public TimeTableConfiguration(int iD, TrainLine trainLine, TimeSpan departureTime, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday)
        {
            ID = iD;
            Deleted = false;
            TrainLine = trainLine;
            DepartureTime = departureTime;
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
            Sunday = sunday;
        }
    }
}
