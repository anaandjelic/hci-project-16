using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils.display
{
    public class SeatDisplay
    {
        public int Number { get; private set; }
        public string SeatClass { get; private set; }
        public bool Occupied { get; private set; }

        public SeatDisplay(int number, string seatClass, bool occupied)
        {
            Number = number;
            SeatClass = seatClass;
            Occupied = occupied;
        }
    }
}
