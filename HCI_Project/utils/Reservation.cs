using System;

namespace HCI_Project.utils
{
    public class Reservation
    {
        public int ID { get; private set; }
        public User Client { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public TrainTimeTable TrainTime { get; private set; }

        public Reservation(int iD, User client, DateTime timeStamp, TrainTimeTable trainTime)
        {
            ID = iD;
            Client = client;
            TimeStamp = timeStamp;
            TrainTime = trainTime;
        }
    }
}
