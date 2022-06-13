using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils.display
{
    public class AvailableSeatDisplay
    {
        public int number { get; private set; }
        public string seatClass { get; private set; }

        public AvailableSeatDisplay(int number, string seatClass)
        {
            this.number = number;
            this.seatClass = seatClass;
        }
    }
}
