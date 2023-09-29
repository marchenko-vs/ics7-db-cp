using BlitzFlug.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BlitzFlug.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private string _connectionString;
        private string _connectionInfo;
        private string _userInfo;

        public UserRepository()
        {
            this._connectionInfo = Startup.ConnectionString;
            var currentUser = SingletonUser.GetInstance();

            if (null == currentUser || null == currentUser.UserInfo)
                this._userInfo = Startup.CustomerData;
            else if ("customer" == currentUser.UserInfo.Role)
                this._userInfo = Startup.CustomerData;
            else if ("moderator" == currentUser.UserInfo.Role)
                this._userInfo = Startup.ModeratorData;
            else
                this._userInfo = Startup.AdminData;

            this._connectionString = this._connectionInfo + this._userInfo;
        }

        public UserRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Users]", connection);

                var dataReader = command.ExecuteReader();

                users = QueryHandler.GetList<User>(dataReader);
            }

            return users;
        }

        public void DeleteUser(Int64 userId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"DELETE FROM [Users] " +
                    $"WHERE Id = @userId", connection);

                command.Parameters.AddWithValue("userId", userId);

                command.ExecuteNonQuery();
            }
        }

        public User GetUserByEmail(string email)
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Users] where [Users].Email = @Email", connection);
                command.Parameters.AddWithValue("Email", email);

                var dataReader = command.ExecuteReader();
                
                users = QueryHandler.GetList<User>(dataReader);

                if (users.Count == 0)
                    return null;
            }

            return users[0];
        }

        public User GetUserById(Int64 userId)
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Users] where [Users].Id = @userId", connection);
                command.Parameters.AddWithValue("userId", userId);

                var dataReader = command.ExecuteReader();

                users = QueryHandler.GetList<User>(dataReader);

                if (users.Count == 0)
                    return null;
            }

            return users[0];
        }

        public void InsertUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
            
                SqlCommand command = new SqlCommand($"INSERT INTO [Users] (Role, Email, Password, FirstName, LastName, RegDate) VALUES " +
                    $"(@Role, @Email, @Password, @FirstName, @LastName, @RegDate)", connection);
                
                command.Parameters.AddWithValue("Role", user.Role);
                command.Parameters.AddWithValue("Email", user.Email);
                command.Parameters.AddWithValue("Password", user.Password);
                if (string.IsNullOrEmpty(user.FirstName))
                    command.Parameters.AddWithValue("FirstName", DBNull.Value);
                else
                    command.Parameters.AddWithValue("FirstName", user.FirstName);
                if (string.IsNullOrEmpty(user.LastName))
                    command.Parameters.AddWithValue("LastName", DBNull.Value);
                else
                    command.Parameters.AddWithValue("LastName", user.LastName);
                command.Parameters.AddWithValue("RegDate", user.RegDate);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"UPDATE Users SET Email = @Email," +
                                                    $"FirstName = @FirstName," +
                                                    $"LastName = @LastName WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("Id", user.Id);
                command.Parameters.AddWithValue("Email", user.Email);
                if (string.IsNullOrEmpty(user.FirstName))
                    command.Parameters.AddWithValue("FirstName", DBNull.Value);
                else
                    command.Parameters.AddWithValue("FirstName", user.FirstName);
                if (string.IsNullOrEmpty(user.LastName))
                    command.Parameters.AddWithValue("LastName", DBNull.Value);
                else
                    command.Parameters.AddWithValue("LastName", user.LastName);

                command.ExecuteReader();
            }
        }
    }
}
