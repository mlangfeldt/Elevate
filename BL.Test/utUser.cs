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
            UserManager.DeleteAll();
            UserManager.Seed();
        }

        [TestMethod]
        public void InsertTest()
        {
            User user = new User
            {
                Email = "123@gmail.com",
                FirstName = "John",
                LastName = "Boy",
                Password = "Hello"
            };

            int result = UserManager.Insert(user);
            Assert.AreEqual(1, result, "Insert method should return 1 when a user is successfully inserted.");
        }

        [TestMethod]
        public void LoadTest()
        {
            var users = UserManager.Load();
            Assert.AreEqual(3, users.Count);
        }

        [TestMethod]
        public void LoginSuccessTest()
        {
            var user = new User { Email = "user", Password = "test" };
            bool result = UserManager.Login(user);

            Assert.IsTrue(result, "Login should return true for valid credentials.");
            Assert.AreEqual("joe", user.FirstName, "FirstName should be 'John' after successful login.");
            Assert.AreEqual("snow", user.LastName, "LastName should be 'Snow' after successful login.");
        }

        [TestMethod]
        [ExpectedException(typeof(LoginFailureException))]
        public void LoginFailureBadPasswordTest()
        {
            var user = new User { Email = "user", Password = "wrongpassword" };
            UserManager.Login(user);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Email was not set.")]
        public void NoEmailTest()
        {
            var user = new User { Email = "", Password = "test" };
            UserManager.Login(user);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Password was not set.")]
        public void MissingPasswordTest()
        {
            var user = new User { Email = "user", Password = "" };
            UserManager.Login(user);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Email could not be found.")]
        public void NoEmailOrPasswordTest()
        {
            var user = new User { Email = "Jim Bob", Password = "testing123" };
            UserManager.Login(user);
        }
    }
}