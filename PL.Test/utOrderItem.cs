using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.PL.Test
{
    [TestClass]
    public class utOrderItem
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

            var rows = dc.tblOrderItems;

            actual = rows.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            // Create a new row
            tblOrderItem row = new tblOrderItem();
            // Set the properties of the new row
            row.Id = -99;
            row.OrderId = 4;
            row.CourseId = 5;
            row.Quantity = 1;
            row.Cost = 15.99;
            // Insert the row into the table
            dc.tblOrderItems.Add(row);
            int results = dc.SaveChanges();
            // Assert
            Assert.IsTrue(results == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Get the row to update
            // Select * from tblOrderItem where id = 2

            tblOrderItem row = dc.tblOrderItems.Where(dt => dt.Id == 2).FirstOrDefault();

            if (row != null)
            {
                // change the values for columns
                row.CourseId = 3;

                int results = dc.SaveChanges();
                Assert.AreNotEqual(0, results);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            // Get the row to update
            // Select * from tblOrderItem where id = 2

            tblOrderItem row = dc.tblOrderItems.Where(dt => dt.Id == 2).FirstOrDefault();

            if (row != null)
            {
                dc.tblOrderItems.Remove(row);

                int results = dc.SaveChanges();
                Assert.AreNotEqual(0, results);
            }
        }
    }
}
