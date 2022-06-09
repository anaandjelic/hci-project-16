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

        //dodato
        private static readonly List<TTT_DTO> TTDTOs = new List<TTT_DTO>();
        private static readonly List<String> TrainLinesString = new List<string>();
        private static readonly List<String> TrainLinesStringWithID = new List<string>();

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
        public static List<string> GetTrainLinesString()
        {
            return TrainLinesString;
        }

        public static List<string> GetTrainLinesStringWithID()
        {
            return TrainLinesStringWithID;
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
        public static void AddTrain(string name, int firstClassCapacity, int secondClassCapacity)
        {
            int id = Trains.Count == 0 ? -1 : Trains.OrderByDescending(x => x.ID).First().ID;
            Trains.Add(new Train(++id, name, firstClassCapacity, secondClassCapacity));
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
            id += 1;
            TrainLines.Add(new TrainLine(id, stations, train));
            //dodato
            TrainLinesString.Add(new TrainLine(id, stations, train).stationsToString());
            TrainLinesStringWithID.Add(new TrainLine(id, stations, train).stationsToStringWithID());
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
            id += 1;

            TimeTables.Add(new TrainTimeTable(id, departureTime, arrivalTime, trainLine));
            //dodato
            TTDTOs.Add(new TTT_DTO(new TrainTimeTable(id, departureTime, arrivalTime, trainLine)));
        }
        public static List<TrainTimeTable> GetTimeTables()
        {
            return TimeTables;
        }

        //dodato
        public static List<TTT_DTO> GetTTT_DTOS()
        {
            return TTDTOs;
        }

        //dodato
        public static TrainLine GetTrainLineByID(int ID)
        {
            foreach(TrainLine trainLine in TrainLines)
            {
                if (trainLine.ID.Equals(ID))
                    return trainLine;
            }
            return null;
        }

        //dodato
        public static List<String> findLinesWithStations(string[] stationNames)
        {
            List<String> TempTrainLines = new List<String>();
            foreach (string tempI in TrainLinesStringWithID)
            {
                bool doesOccur = false;
                foreach (string tempJ in stationNames)
                {
                    if (!tempI.ToLower().Contains(tempJ.ToLower()))
                    {
                        doesOccur = false;
                        break;
                    }
                    else
                        doesOccur = true;
                }
                if (doesOccur)
                    TempTrainLines.Add(tempI);
            }
            return TempTrainLines;
        }
    }
}
