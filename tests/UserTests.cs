using BlitzFlug.Models;
using Moq;
using BlitzFlug.Repositories;

namespace BlitzFlugUnitTests
{
    public class UserTests
    {
        [Fact]
        public void RegisterNewUser()
        {
            string email = "testEmail@test.com";
            string password = "testPassword123";
            var mock = new Mock<IUserRepository<User>>();

            mock.Setup(repo => repo.GetUserByEmail(email)).Returns(GetNonExistentUserByEmail(email));
            mock.Setup(repo => repo.InsertUser(new User())).Callback(Insert);

            var user = new User(mock.Object);

            user.Register(email, password);

            Assert.Equal(email, user.Email);
            Assert.NotEqual(password, user.Password);
        }

        [Fact]
        public void RegisterExistingUser()
        {
            string email = "testEmail@test.com";
            string password = "testPassword123";
            var mock = new Mock<IUserRepository<User>>();

            mock.Setup(repo => repo.GetUserByEmail(email)).Returns(GetExistingUserByEmail(email));
            mock.Setup(repo => repo.InsertUser(new User())).Callback(Insert);

            var user = new User(mock.Object);

            Action act = () => user.Register(email, password);

            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void LoginNonExistentUser()
        {
            string email = "testEmail@test.com";
            string password = "testPassword123";
            var mock = new Mock<IUserRepository<User>>();

            mock.Setup(repo => repo.GetUserByEmail(email)).Returns(GetNonExistentUserByEmail(email));

            var user = new User(mock.Object);

            Assert.Equal(-1, user.Login(user.Email, user.Password));
        }

        private User GetNonExistentUserByEmail(string email) => null;

        private User GetExistingUserByEmail(string email) => new User() { Email = "testEmail@test.com", Password = "testPassword123" };

        private void Insert() { }
    }
}
