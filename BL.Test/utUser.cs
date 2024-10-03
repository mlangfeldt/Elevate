using BL;
using BL.Models;

namespace Elevate.BL.Test
{
    [TestClass]
    public class utUser
    {
        [TestInitialize]
        public void TestInitialize()
        {
            // Ensure the database is in a known state before each test
            UserManager.DeleteAll();
            UserManager.Seed();
        }

        [TestMethod]
        public void InsertTest()
        {
            // Arrange
            User user = new User
            {
                Email = "123@gmail.com",
                FirstName = "John",
                LastName = "Boy",
                Password = "Hello"
            };

            // Act
            int result = UserManager.Insert(user);

            // Assert
            Assert.AreEqual(1, result, "Insert method should return 1 when a user is successfully inserted.");
        }

        [TestMethod]
        public void LoadTest()
        {
            var users = UserManager.Load();

            // Assert
            Assert.AreEqual(3, users.Count);
        }

        [TestMethod]
        public void LoginSuccessTest()
        {
            // Arrange
            var user = new User { Email = "user", Password = "test" };

            // Act
            bool result = UserManager.Login(user);

            // Assert
            Assert.IsTrue(result, "Login should return true for valid credentials.");
            Assert.AreEqual("John", user.FirstName, "FirstName should be 'John' after successful login.");
            Assert.AreEqual("Snow", user.LastName, "LastName should be 'Snow' after successful login.");
        }

        [TestMethod]
        [ExpectedException(typeof(LoginFailureException))]
        public void LoginFailureBadPasswordTest()
        {
            // Arrange
            var user = new User { Email = "user", Password = "wrongpassword" };

            // Act
            UserManager.Login(user);

            // Assert is handled by ExpectedException attribute
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Email was not set.")]
        public void NoEmailTest()
        {
            // Arrange
            var user = new User { Email = "", Password = "test" };

            // Act
            UserManager.Login(user);

            // Assert is handled by ExpectedException attribute
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Password was not set.")]
        public void MissingPasswordTest()
        {
            // Arrange
            var user = new User { Email = "user", Password = "" };

            // Act
            UserManager.Login(user);

            // Assert is handled by ExpectedException attribute
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Email could not be found.")]
        public void NoEmailOrPasswordTest()
        {
            // Arrange
            var user = new User { Email = "Jim Bob", Password = "testing123" };

            // Act
            UserManager.Login(user);

            // Assert is handled by ExpectedException attribute
        }
    }
}
