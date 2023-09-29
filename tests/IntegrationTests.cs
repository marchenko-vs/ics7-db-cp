using BlitzFlug.Models;
using Moq;
using BlitzFlug.Repositories;

namespace BlitzFlugUnitTests
{
    public class IntergrationTests
    {
        [Fact]
        public void RegisterNewUser()
        {
            string email = "testEmail@test.com";
            string password = "testPassword123";

            var user = new User();

            user.Register(email, password);

            Assert.Equal(email, user.Email);
            Assert.NotEqual(password, user.Password);
        }

        [Fact]
        public void RegisterExistingUser()
        {
            string email = "testEmail@test.com";
            string password = "testPassword123";

            var user = new User();

            Action act = () => user.Register(email, password);

            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void LoginExistingUser()
        {
            string email = "testEmail@test.com";
            string password = "testPassword123";

            var user = new User();

            Assert.Equal(0, user.Login(email, password));
        }

        [Fact]
        public void LoginNonExistentUser()
        {
            string email = "falseEmail";
            string password = "flasePassword";

            var user = new User();

            Assert.Equal(-1, user.Login(email, password));
        }

        [Fact]
        public void LoginIncorrectPassword()
        {
            string email = "testEmail@test.com";
            string password = "flasePassword";

            var user = new User();

            Assert.Equal(-2, user.Login(email, password));
        }
    }
}
