using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.PL.Test
{
    [TestClass]
    public class utCustomer
    {
        protected ElevateEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new ElevateEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
        }

        [TestMethod]
        public void LoadTest()
        {
            int expected = 3;

            int actual = 0;

            var rows = dc.tblCustomers;

            actual = rows.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            // Create a new row
            tblCustomer row = new tblCustomer();
            // Set the properties of the new row
            row.Id = -99;
            row.FirstName = "John";
            row.LastName = "Johnson";
            row.Email = "hi@hi.com";
            row.UserId = 4;
            // Insert the row into the table
            dc.tblCustomers.Add(row);
            int results = dc.SaveChanges();
            // Assert
            Assert.IsTrue(results == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Get the row to update
            // Select * from tblCustomer where id = 2

            tblCustomer row = dc.tblCustomers.Where(dt => dt.Id == 2).FirstOrDefault();

            if (row != null)
            {
                // change the values for columns
                row.UserId = 1;

                int results = dc.SaveChanges();
                Assert.AreNotEqual(0, results);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            // Get the row to update
            // Select * from tblCustomer where id = 2

            tblCustomer row = dc.tblCustomers.Where(dt => dt.Id == 2).FirstOrDefault();

            if (row != null)
            {
                dc.tblCustomers.Remove(row);

                int results = dc.SaveChanges();
                Assert.AreNotEqual(0, results);
            }
        }
    }
}
