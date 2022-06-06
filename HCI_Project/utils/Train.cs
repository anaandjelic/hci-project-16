namespace HCI_Project.utils
{
    public class Train
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int FirstClassCapacity { get; set; }
        public int SecondClassCapacity { get; set; }
        public Train() { }
        public Train(int id, string name, int firstClassCapacity, int secondClassCapacity)
        {
            ID = id;
            Name = name;
            FirstClassCapacity = firstClassCapacity;
            SecondClassCapacity = secondClassCapacity;
        }
    }
}
