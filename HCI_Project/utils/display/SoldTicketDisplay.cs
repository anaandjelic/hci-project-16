using HCI_Project.clientPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils.display
{
    public class SoldTicketDisplay
    {
        public string Customer { get; set; }
        public string PurchaseDate { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureDestination { get; set; }
        public string LeavesAt { get; set; }
        public string ArrivesAt { get; set; }
        public string Price { get; set; }

        public SoldTicketDisplay(Ticket ticket)
        {
            Customer = $"{ticket.Client.FirstName} {ticket.Client.LastName}";
            PurchaseDate = ticket.TimeStamp.ToShortDateString();
            DepartureDate = ticket.TrainTime.DepartureDate.ToShortDateString();
            DepartureDestination = $"{ticket.FromStation.Name} → {ticket.ToStation.Name}";
            LeavesAt = ticket.Departure.ToShortTimeString();
            ArrivesAt = ticket.Arrival.ToShortTimeString();
            Price = $"{ticket.Price} RSD";
        }
    }
}
