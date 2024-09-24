using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.PL.Test
{
    [TestClass]
    public class utUser
    {
        //protected ElevateEntities dc;
        //protected IDbContextTransaction transaction;

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    dc = new ElevateEntities();
        //    transaction = dc.Database.BeginTransaction();
        //}

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    transaction.Rollback();
        //    transaction.Dispose();
        //}

        //[TestMethod]
        //public void LoadTest()
        //{
        //    int expected = 3;

        //    int actual = 0;

        //    // var dc = new ElevateEntities();

        //    var rows = dc.tblUsers;

        //    actual = rows.Count();

        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod]
        //public void InsertTest()
        //{
        //    // Create a new row
        //    tblUser row = new tblUser();
        //    // Set the properties of the new row
        //    row.Id = -99;
        //    row.FirstName = "Jim";
        //    row.LastName = "Jimmies";
        //    row.Email = "hello@gmail.com";
        //    row.Password = "chicken";
        //    // Insert the row into the table
        //    dc.tblUsers.Add(row);
        //    int results = dc.SaveChanges();
        //    // Assert
        //    Assert.IsTrue(results == 1);
        //}

        //[TestMethod]
        //public void UpdateTest()
        //{
        //    // Get the row to update
        //    // Select * from tblUser where id = 2

        //    tblUser row = dc.tblUsers.Where(dt => dt.Id == 2).FirstOrDefault();

        //    if (row != null)
        //    {
        //        // change the values for columns
        //        row.FirstName = "Jack";
        //        row.LastName = "Jackson";

        //        int results = dc.SaveChanges();
        //        Assert.AreNotEqual(0, results);
        //    }

        //}

        //[TestMethod]
        //public void DeleteTest()
        //{
        //    // Get the row to update
        //    // Select * from tblUser where id = 2

        //    tblUser row = dc.tblUsers.Where(dt => dt.Id == 2).FirstOrDefault();

        //    if (row != null)
        //    {
        //        dc.tblUsers.Remove(row);

        //        int results = dc.SaveChanges();
        //        Assert.AreNotEqual(0, results);
        //    }
        //}
    }
}
