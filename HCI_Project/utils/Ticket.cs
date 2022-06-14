using System;

namespace HCI_Project.utils
{
    public class Ticket
    {
        public int ID { get; private set; }
        public User Client { get; private set; }
        public bool Deleted { get; set; }
        public bool Purchased { get; set; }
        public double Price { get; private set; }
        public int Seat { get; private set; }
        public String SeatClass { get; private set; }
        public DateTime TimeStamp { get; set; }
        public Station FromStation { get; set; }
        public Station ToStation { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public TrainTimeTable TrainTime { get; private set; }

        public Ticket(int iD, User client, double price, int seat, String seatClass, bool bought, 
            Station from, Station to, DateTime departure, DateTime arrival, TrainTimeTable trainTime)
        {
            ID = iD;
            Deleted = false;
            Client = client;
            Price = price;
            Seat = seat;
            SeatClass = seatClass;
            TimeStamp = DateTime.Now;
            TrainTime = trainTime;
            FromStation = from;
            ToStation = to;
            Departure = departure;
            Arrival = arrival;
            Purchased = bought;
        }
    }
}
