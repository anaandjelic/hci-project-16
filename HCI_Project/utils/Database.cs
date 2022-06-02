using System;
using System.Collections.Generic;
using System.Linq;

namespace HCI_Project.utils
{
    public static class Database
    {
        private static readonly List<User> Users = new List<User>();
        private static readonly List<Train> Trains = new List<Train>();
        private static readonly List<TrainLine> TrainLines = new List<TrainLine>();
        private static readonly List<TrainTimeTable> TimeTables = new List<TrainTimeTable>();
        private static User LoggedInUser;

        public static void LogOut()
        {
            LoggedInUser = null;
        }
        public static UserRole LogIn(string username, string password)
        {
            User user = (User)(from u in Users where u.Username == username && u.Password == password select u).FirstOrDefault();
            LoggedInUser = user ?? throw new UserNotFoundException();
            return user.Role;
        }
        public static List<User> GetUsers()
        {
            return Users;
        }
        public static List<Train> GetTrains()
        {
            return Trains;
        }
        public static List<TrainLine> GetTrainLines()
        {
            return TrainLines;
        }

        // User CRUD
        public static void DeleteUser(string username)
        {
            User user = (User)(from u in Users where u.Username == username select u).FirstOrDefault();
            if (user == null)
                throw new UserNotFoundException();
            Users.Remove(user);
        }
        public static void AddUser(string username, string password, string firstName, string lastName, UserRole role)
        {
            User user = (User)(from u in Users where u.Username == username select u).FirstOrDefault();
            if (user != null)
                throw new ExistingUsernameException();
            Users.Add(new User(username, password, firstName, lastName, role));
        }

        // Train CRUD
        public static void DeleteTrain(int ID)
        {
            Train train = (Train)(from t in Trains where t.ID == ID select t);
            if (train == null)
                throw new TrainNotFoundException();
            Trains.Remove(train);
        }
        public static void AddTrain(int capacity, string name)
        {
            int id = Trains.Count == 0 ? -1 : Trains.OrderByDescending(x => x.ID).First().ID;
            Trains.Add(new Train(++id, name, capacity));
        }
        public static Train GetTrain(int ID)
        {
            Train train = (Train)(from t in Trains where t.ID == ID select t).FirstOrDefault();
            if (train == null)
                throw new TrainNotFoundException();
            return train;
        }

        // TrainLine CRUD
        public static void AddTrainLine(List<Station> stations, Train train)
        {
            int id = TrainLines.Count == 0 ? -1 : TrainLines.OrderByDescending(x => x.ID).First().ID;
            TrainLines.Add(new TrainLine(++id, stations, train));
        }
        public static TrainLine GetTrainLine(int ID)
        {
            TrainLine trainLine = (TrainLine)(from t in TrainLines where t.ID == ID select t).FirstOrDefault();
            if (trainLine == null)
                throw new TrainNotFoundException();
            return trainLine;
        }

        // TrainTimeTable CRUD
        public static void AddTimeTable(DateTime departureTime, DateTime arrivalTime, TrainLine trainLine)
        {
            int id = TimeTables.Count == 0 ? -1 : TimeTables.OrderByDescending(x => x.ID).First().ID;
            TimeTables.Add(new TrainTimeTable(++id, departureTime, arrivalTime, trainLine));
        }
        public static List<TrainTimeTable> GetTimeTables()
        {
            return TimeTables;
        }
    }
}
