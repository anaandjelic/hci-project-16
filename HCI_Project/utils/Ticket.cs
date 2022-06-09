using System;

namespace HCI_Project.utils
{
    public class Ticket
    {
        public int ID { get; private set; }
        public User Client { get; private set; }
        public bool Deleted { get; set; }
        public double Price { get; private set; }
        public int Seat { get; private set; }
        public DateTime TimeStamp { get; set; }
        public TrainTimeTable TrainTime { get; private set; }

        public Ticket(int iD, User client, double price, int seat, TrainTimeTable trainTime)
        {
            ID = iD;
            Deleted = false;
            Client = client;
            Price = price;
            Seat = seat;
            TimeStamp = DateTime.Now;
            TrainTime = trainTime;
        }
    }
}
