using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils
{
    public class Ticket
    {
        public int ID { get; private set; }
        public User Client { get; private set; }
        public double Price { get; private set; }
        public string Seat { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public TrainTimeTable TrainTime { get; private set; }

        public Ticket(int iD, User client, double price, string seat, TrainTimeTable trainTime)
        {
            ID = iD;
            Client = client;
            Price = price;
            Seat = seat;
            TimeStamp = DateTime.Now;
            TrainTime = trainTime;
        }
    }
}
