using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.PL.Test
{
    [TestClass]
    public class utOrder
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

            // var dc = new ElevateEntities();

            var rows = dc.tblOrders;

            actual = rows.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            // Create a new row
            tblOrder row = new tblOrder();
            // Set the properties of the new row
            row.Id = -99;
            row.CustomerId = 4;
            row.OrderDate = new DateTime(2020, 5, 25);
            row.UserId = 4;
            // Insert the row into the table
            dc.tblOrders.Add(row);
            int results = dc.SaveChanges();
            // Assert
            Assert.IsTrue(results == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Get the row to update
            // Select * from tblOrder where id = 2

            tblOrder row = dc.tblOrders.Where(dt => dt.Id == 2).FirstOrDefault();

            if (row != null)
            {
                // change the values for columns
                row.CustomerId = 3;

                int results = dc.SaveChanges();
                Assert.AreNotEqual(0, results);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            // Get the row to update
            // Select * from tblOrder where id = 2

            tblOrder row = dc.tblOrders.Where(dt => dt.Id == 2).FirstOrDefault();

            if (row != null)
            {
                dc.tblOrders.Remove(row);

                int results = dc.SaveChanges();
                Assert.AreNotEqual(0, results);
            }
        }
    }
}
