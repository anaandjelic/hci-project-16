using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils
{
    public class Train
    {
        public int ID { get; private set; }
        public int Capacity { get; set; }
        public Train() { }
        public Train(int id, int capacity)
        {
            ID = id;
            Capacity = capacity;
        }
    }
}
