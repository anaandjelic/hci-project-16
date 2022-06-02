using System;

namespace HCI_Project.utils
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() { }
    }
    public class ExistingUsernameException : Exception
    {
        public ExistingUsernameException() { }
    }
    public class TrainNotFoundException : Exception
    {
        public TrainNotFoundException() { }
    }
}
