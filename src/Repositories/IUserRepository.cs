namespace BlitzFlug.Repositories
{
    public interface IUserRepository<T>
        where T : class
    {
        public IEnumerable<T> GetAllUsers();
        public T GetUserById(Int64 userId);
        public T GetUserByEmail(string email);
        public void InsertUser(T user);
        public void UpdateUser(T user);
        public void DeleteUser(Int64 userId);
    }
}
