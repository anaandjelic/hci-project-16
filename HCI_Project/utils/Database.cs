using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.utils
{
    class Database
    {
        private static readonly List<User> Users = new List<User>();
        private static readonly List<Train> Trains = new List<Train>();
        private static readonly List<TrainLine> TrainLines = new List<TrainLine>();

        public List<User> GetUsers()
        {
            return Users;
        }
        public List<Train> GetTrains()
        {
            return Trains;
        }
        public List<TrainLine> GetTrainLines()
        {
            return TrainLines;
        }

        // User CRUD
        public void DeleteUser(string username)
        {
            User user = (User)(from u in Users where u.Username == username select u);
            if (user == null)
                throw new UserNotFoundException();
            Users.Remove(user);
        }
        public User GetUser(string username, string password)
        {
            User user = (User)(from u in Users where u.Username == username && u.Password == password select u);
            if (user == null)
                throw new UserNotFoundException();
            return user;
        }
        public void AddUser(string username, string password, string firstName, string lastName)
        {
            User user = (User)(from u in Users where u.Username == username select u);
            if (user != null)
                throw new ExistingUsernameException();
            Users.Add(new User(username, password, firstName, lastName));
        }

        // Train CRUD
        public void DeleteTrain(int ID)
        {
            Train train = (Train)(from t in Trains where t.ID == ID select t);
            if (train == null)
                throw new TrainNotFoundException();
            Trains.Remove(train);
        }
        public void Addtrain(int capacity)
        {
            var id = Trains.OrderByDescending(x => x.ID).First().ID;
            Trains.Add(new Train(++id, capacity));
        }

        // TrainLine CRUD

    }
}
