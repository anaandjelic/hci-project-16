﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils.display
{
    public class TicketDisplay
    {
        public string Date { get; set; }
        public string DepartureDestination { get; set; }
        public string LeavesAt { get; set; }
        public string ArrivesAt { get; set; }
        public string Seat { get; set; }
        public string Price { get; set; }
        public string Bought { get; set; }
        public Ticket OriginalTicket { get; set; }

        public TicketDisplay(Ticket ticket)
        {
            Date = ticket.TrainTime.DepartureDate.ToShortDateString();
            DepartureDestination = $"{ticket.FromStation.Name} → {ticket.ToStation.Name}";
            LeavesAt = ticket.Departure.ToShortTimeString();
            ArrivesAt = ticket.Arrival.ToShortTimeString();
            Seat = $"{ticket.Seat} ({ticket.SeatClass})";
            Price = $"{ticket.Price} RSD";
            Bought = ticket.Bought ? "Bought" : "Reserved";
        }
    }
}
