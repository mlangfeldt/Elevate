using BL;
using BL.Models;

namespace Elevate.BL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void Seed()
        {
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

            Assert.AreEqual(1, UserManager.Insert(user, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(1, UserManager.Load().Count);
        }

        [TestInitialize]
        public void Initialize()
        {
            UserManager.Seed();
        }

        [TestMethod]
        public void LoginSuccessTest()
        {
            Assert.IsTrue(UserManager.Login(new User { Email = "dpeerenboom@gmail.com", Password = "maple" }));
            Assert.IsTrue(UserManager.Login(new User { Email = "bfoote", Password = "maple" }));
        }
        [TestMethod]
        public void LoginFailureBadPasswordTest()
        {
            try
            {
                Assert.IsTrue(UserManager.Login(new User { Email = "dpeerenboom", Password = "pine" }));
            }
            catch (LoginFailureException)
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        [TestMethod]
        public void LoginFailureMissingIdTest()
        {
            try
            {
                Assert.IsTrue(UserManager.Login(new User { Email = "", Password = "maple" }));
            }
            catch (LoginFailureException)
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        [TestMethod]
        public void LoginFailureMissingPasswordTest()
        {
            try
            {
                Assert.IsTrue(UserManager.Login(new User { Email = "dpeerenboom", Password = "" }));
            }
            catch (LoginFailureException)
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        [TestMethod]
        public void LoginFailureUnknownUserTest()
        {
            try
            {
                Assert.IsTrue(UserManager.Login(new User { Email = "Jim Bob", Password = "maple" }));
            }
            catch (LoginFailureException)
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
