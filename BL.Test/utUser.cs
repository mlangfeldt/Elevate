using BL.Models;

namespace Elevate.BL.Test
{
    [TestClass]
    public class utUser
    {

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

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, UserManager.Load().Count);
        }

        [TestMethod]
        public void LoginSuccessTest()
        {
            Seed();
            Assert.IsTrue(UserManager.Login(new User { Email = "user", Password = "test" }));
        }
        public void Seed()
        {
            UserManager.Seed();
        }

        [TestMethod]
        public void LoginFailureBadPasswordTest()
        {
            try
            {
                Assert.IsFalse(UserManager.Login(new User { Email = "Joe", Password = "pine" }));
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
                Assert.IsFalse(UserManager.Login(new User { Email = "", Password = "Smith" }));
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
                Seed();
                Assert.IsFalse(UserManager.Login(new User { Email = "Joe", Password = "" }));
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
                Seed();
                Assert.IsFalse(UserManager.Login(new User { Email = "Jim Bob", Password = "testing123" }));
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
        public void LoginFailureNoPassword()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User { Email = "user", Password = "" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }
}
