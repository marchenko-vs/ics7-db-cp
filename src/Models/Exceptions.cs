namespace BlitzFlug.Models
{
    public class ExistingUserException : Exception
    {
        public ExistingUserException(string info) : base(info)
        { 
        
        }
    }

    public class NotExistingUserException : Exception
    {
        public NotExistingUserException(string info) : base(info)
        {

        }
    }

    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException(string info) : base(info)
        {

        }
    }

    public class NoTicketsException : Exception
    {
        public NoTicketsException(string info) : base(info)
        {

        }
    }

    public class NotLoggedInException : Exception
    {
        public NotLoggedInException(string info) : base(info)
        {

        }
    }
}