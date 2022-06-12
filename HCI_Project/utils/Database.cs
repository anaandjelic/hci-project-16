using HCI_Project.utils.display;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HCI_Project.utils
{
    public static class Database
    {
        private static readonly List<User> Users = new List<User>();
        private static readonly List<Train> Trains = new List<Train>();
        private static readonly List<Ticket> Tickets = new List<Ticket>();
        private static readonly List<TrainLine> TrainLines = new List<TrainLine>();
        private static readonly List<TrainTimeTable> TimeTables = new List<TrainTimeTable>();
        private static readonly List<TimeTableConfiguration> Configurations = new List<TimeTableConfiguration>();
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
            User user = (from u in Users where u.Username == username && u.Password == password select u).FirstOrDefault();
            LoggedInUser = user ?? throw new UserNotFoundException();
            return user.Role;
        }
        public static List<User> GetUsers()
        {
            return Users;
        }
        public static List<Train> GetTrains()
        {
            return Trains.Where(x => !x.Deleted).ToList();
        }
        public static List<TrainLine> GetTrainLines()
        {
            return TrainLines.Where(x => !x.Deleted).ToList();
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
        public static void AddUser(string username, string password, string firstName, string lastName, UserRole role)
        {
            User user = (User)(from u in Users where u.Username == username select u).FirstOrDefault();
            if (user != null)
                throw new ExistingUsernameException();
            Users.Add(new User(username, password, firstName, lastName, role));
        }

        public static TrainTimeTable GetTimeTable(TimeTableConfiguration config, DateTime date)
        {
            return TimeTables.FirstOrDefault(x => x.Configuration.ID == config.ID &&
                                                  x.DepartureDate.Date == date.Date &&
                                                  !x.Deleted);
        }

        // Train CRUD
        public static void DeleteTrain(Train train)
        {
            var timeTables = TimeTables.Where(x => x.TrainLine.Train.ID == train.ID && x.DepartureDate > DateTime.Now.AddDays(5));
            foreach (var timetable in timeTables)
            {
                var tickets = Tickets.Where(x => x.TrainTime.ID == timetable.ID).ToList();
                tickets.ForEach(x => x.Deleted = true);
                timetable.Deleted = true;
                timetable.TrainLine.Deleted = true;
                timetable.Configuration.Deleted = true;
            }
            Trains.Remove(train);
        }

        internal static void DeleteTimetable(TrainTimeTable timetable)
        {
            timetable.Deleted = true;
            Tickets.Where(x => x.TrainTime == timetable && x.TimeStamp > DateTime.Now.AddDays(5)).ToList().ForEach(x => x.Deleted = true);
        }

        internal static void ChangeTimeTable(TrainTimeTable timetable, DateTime time)
        {
            Tickets.Where(x => x.TrainTime == timetable && x.TimeStamp > DateTime.Now.AddDays(5)).ToList().ForEach(x => x.TimeStamp += time.TimeOfDay);
        }

        public static void AddTrain(string name, int firstClassCapacity, int secondClassCapacity)
        {
            int id = Trains.Count == 0 ? -1 : Trains.OrderByDescending(x => x.ID).First().ID;
            Trains.Add(new Train(++id, name, firstClassCapacity, secondClassCapacity));
        }

        internal static void EditTrain(Train train)
        {
            Tickets.Where(x => x.TrainTime.TrainLine.Train.ID == train.ID &&
                               x.TrainTime.DepartureDate > DateTime.Now.AddDays(5) &&
                               x.Seat > train.FirstClassCapacity + train.SecondClassCapacity)
                   .ToList()
                   .ForEach(x => x.Deleted = true);
        }

        public static Train GetTrain(int ID)
        {
            Train train = Trains.Where(x => x.ID == ID && !x.Deleted).FirstOrDefault();
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
            TrainLine trainLine = TrainLines.Where(x => x.ID == ID && !x.Deleted).FirstOrDefault();
            if (trainLine == null)
                throw new TrainNotFoundException();
            return trainLine;
        }

        // TrainTimeTable CRUD
        public static void AddTimeTable(DateTime departureTime, TrainLine trainLine, TimeTableConfiguration configuration)
        {
            int id = TimeTables.Count == 0 ? -1 : TimeTables.OrderByDescending(x => x.ID).First().ID;
            id += 1;

            TimeTables.Add(new TrainTimeTable(id, departureTime, trainLine, configuration));
            //dodato
            TTDTOs.Add(new TTT_DTO(new TrainTimeTable(id, departureTime, trainLine, configuration)));
        }

        internal static void DeleteConfiguration(TimeTableConfiguration config)
        {
            config.Deleted = true;
            var timetables = TimeTables.Where(x => x.Configuration == config &&
                                                   x.DepartureDate > DateTime.Now.AddDays(5));
            foreach (var timetable in timetables)
            {
                Tickets.Where(x => x.TrainTime.ID == timetable.ID).ToList()
                    .ForEach(x => x.Deleted = true);
                timetable.Deleted = true;
            }
        }

        public static List<TrainTimeTable> GetTimeTables()
        {
            return TimeTables.Where(x => !x.Deleted).ToList();
        }

        public static TimeTableConfiguration GetConfiguration(TrainLine trainLine)
        {
            return Configurations.Where(x => !x.Deleted && x.TrainLine.ID == trainLine.ID).FirstOrDefault();
        }

        public static TimeTableConfiguration AddConfiguration(TrainLine trainLine, TimeSpan departureTime, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday)
        {
            int id = TrainLines.Count == 0 ? -1 : TrainLines.OrderByDescending(x => x.ID).First().ID;
            var config = (from c in Configurations where c.TrainLine.ID == trainLine.ID select c).FirstOrDefault();
            if (config != null) 
            { 
                Configurations.Remove(config);
                id = config.ID - 1;
            }
            config = new TimeTableConfiguration(++id, trainLine, departureTime, monday, tuesday, wednesday, thursday, friday, saturday, sunday);
            Configurations.Add(config);
            return config;
        }

        public static List<TrainLine> GetConfiguredTrainLines()
        {
            return (from c in Configurations where !c.Deleted select c.TrainLine).ToList();
        }

        public static List<TrainLine> GetUnConfiguredTrainLines()
        {
            return TrainLines.Except(GetConfiguredTrainLines()).Where(x => !x.Deleted).ToList();
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
                if (trainLine.ID.Equals(ID) && !trainLine.Deleted)
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

        // diaplays

        private static int GetAvailableSeats(TrainTimeTable timeTable)
        {
            return timeTable.TrainLine.Train.GetAllSeats() - Tickets.Where(x => x.TrainTime.ID == timeTable.ID).Count();
        }

        public static List<TimeTableDisplay> GetTimeTableDisplays()
        {
            var res = new List<TimeTableDisplay>();
            TimeTables.ForEach(x => res.Add(new TimeTableDisplay(x, "Novi Sad", "Belgrade", GetAvailableSeats(x))));
            return res;
        }

        public static List<TimeTableDisplay> GetTimeTableDisplays(string from, string to, DateTime date)
        {
            var res = new List<TimeTableDisplay>();
            var timeTables = TimeTables.Where(x => x.DepartureDate.Date.Equals(date.Date) &&
                                              x.TravelsBetween(from, to))
                .ToList();
            timeTables.ForEach(x => res.Add(new TimeTableDisplay(x, from, to, GetAvailableSeats(x))));
            return res;
        }
    }
}
