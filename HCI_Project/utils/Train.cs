namespace HCI_Project.utils
{
    public class Train
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Capacity { get; set; }
        public Train() { }
        public Train(int id, string name, int capacity)
        {
            ID = id;
            Name = name;
            Capacity = capacity;
        }
    }
}
