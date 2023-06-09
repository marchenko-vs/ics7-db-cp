using BlitzFlug.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BlitzFlug.Models
{
    public class User
    {
        private IUserRepository<User> _db;

        [Key]
        public Int64 Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // encrypted password
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegDate { get; set; }

        public User()
        {
            this.Role = "customer";
            this.Email = string.Empty;
            this.Password = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.RegDate = DateTime.Now;

            var factory = new MsSqlRepositoryFactory();
            this._db = factory.CreateUserRepository();
        }

        public User(IUserRepository<User> db)
        {
            this.Role = "customer";
            this.Email = string.Empty;
            this.Password = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.RegDate = DateTime.Now;

            this._db = db;
        }

        private static string EncryptPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        private bool Verify(string email, string password)
        {
            string savedPasswordHash = this._db.GetUserByEmail(email).Password;

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash1 = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash1[i])
                {
                    return false;
                }
            }

            return true;
        }

        public void Register(string email, string password)
        {
            if (null != this._db.GetUserByEmail(email))
            {
                throw new ExistingUserException(String.Format("Пользователь {0} уже существует!", email));
            }

            this.Email = email;
            this.Password = EncryptPassword(password);

            this._db.InsertUser(this);
        }

        public void Login(string email, string password)
        {
            if (null == this._db.GetUserByEmail(email))
            {
                throw new NotExistingUserException(String.Format("Пользователь {0} не найден!", email));
            }

            if (false == this.Verify(email, password))
            {
                throw new IncorrectPasswordException(String.Format("Неверный пароль!", email));
            }
        }

        public void ChangeProfile()
        {
            var singletonUser = SingletonUser.GetInstance();
            var currentUser = this.GetCurrentUser(singletonUser.UserInfo.Email);

            if (null == currentUser)
                throw new NotLoggedInException("Ошибка аутентификации");

            this.Id = currentUser.Id;

            if (string.IsNullOrEmpty(this.Email))
                this.Email = currentUser.Email;

            if (string.IsNullOrEmpty(this.FirstName))
                this.FirstName = currentUser.FirstName;

            if (string.IsNullOrEmpty(this.LastName))
                this.LastName = currentUser.LastName;

            try
            {
                this._db.UpdateUser(this);
            }
            catch (Exception)
            {
                throw new ExistingUserException(String.Format("Почта {0} уже занята!", this.Email));
            }
        }

        public Int64 GetCurrentId(string email)
        {
            return this._db.GetUserByEmail(email).Id;
        }

        public User GetCurrentUser(string email)
        {
            return this._db.GetUserByEmail(email);
        }

        public List<User> GetAllUsers()
        {
            var currentUser = SingletonUser.GetInstance();
            List<User> users = this._db.GetAllUsers().ToList();

            var itemToRemove = users.Single(r => r.Id == currentUser.UserInfo.Id);
            users.Remove(itemToRemove);

            return users;
        }

        public void DeleteUser()
        {
            this._db.DeleteUser(this.Id);
        }
    }
}
