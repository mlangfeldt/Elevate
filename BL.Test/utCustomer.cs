using Elevate.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.BL.Test
{
    [TestClass]
    public class utCustomer
    {
        [TestMethod]
        public void InsertTest()
        {
            Customer customer = new Customer
            {
                FirstName = "Jacob",
                LastName = "Peters",
                Email = "hi@hello.com",
                UserId = 1234
            };

            Assert.AreEqual(1, CustomerManager.Insert(customer, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, CustomerManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(1, CustomerManager.LoadById(1).Id);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Customer customer = CustomerManager.LoadById(3);
            customer.FirstName = "Andrew";
            Assert.IsTrue(CustomerManager.Update(customer, true) > 0);
        }
        [TestMethod]
        public void DeleteTest()
        {
            Assert.AreEqual(1, CustomerManager.Delete(2, true));
        }
    }
}
