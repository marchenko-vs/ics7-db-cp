namespace BlitzFlug.Models
{
    class SingletonUser
    {
        private static SingletonUser _currentUser;
        public User UserInfo { get; set; }

        private SingletonUser(User user)
        { 
            this.UserInfo = user;
        }

        public static SingletonUser GetInstance()
        {
            return _currentUser;
        }

        public static SingletonUser GetInstance(User user)
        {
            if (_currentUser == null)
                _currentUser = new SingletonUser(user);

            return _currentUser;
        }
    }
}
