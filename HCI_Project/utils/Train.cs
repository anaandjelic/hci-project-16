namespace HCI_Project.utils
{
    public class Train
    {
        public int ID { get; private set; }
        public string Name { get; set; }
        public int FirstClassCapacity { get; set; }
        public int SecondClassCapacity { get; set; }
        public bool Deleted { get; set; }
        public Train() { }
        public Train(int id, string name, int firstClassCapacity, int secondClassCapacity)
        {
            ID = id;
            Name = name;
            FirstClassCapacity = firstClassCapacity;
            SecondClassCapacity = secondClassCapacity;
        }

        public override string ToString()
        {
            return Name;
        }
        public int GetAllSeats()
        {
            return SecondClassCapacity + FirstClassCapacity;
        }
    }
}
