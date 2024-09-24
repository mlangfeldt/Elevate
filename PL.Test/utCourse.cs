using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.PL.Test
{
    [TestClass]
    public class utCourse
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

        //    // var dc = new DVDCentralEntities();

        //    var rows = dc.tblCourses;

        //    actual = rows.Count();

        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod]
        //public void InsertTest()
        //{
        //    // Create a new row
        //    tblCourse row = new tblCourse();
        //    // Set the properties of the new row
        //    row.Id = -99;
        //    row.Name = "test";
        //    row.Description = "tarzan is the man";
        //    // Insert the row into the table
        //    dc.tblCourses.Add(row);
        //    int results = dc.SaveChanges();
        //    // Assert
        //    Assert.IsTrue(results == 1);
        //}

        //[TestMethod]
        //public void UpdateTest()
        //{
        //    // Get the row to update
        //    // Select * from tblCourse where id = 2

        //    tblCourse row = dc.tblCourses.Where(dt => dt.Id == 2).FirstOrDefault();

        //    if (row != null)
        //    {
        //        // change the values for columns
        //        row.Name = "test";
        //        row.Description = "cheddar";

        //        int results = dc.SaveChanges();
        //        Assert.AreNotEqual(0, results);
        //    }

        //}

        //[TestMethod]
        //public void DeleteTest()
        //{
        //    // Get the row to update
        //    // Select * from tblCourse where id = 2

        //    tblCourse row = dc.tblCourses.Where(dt => dt.Id == 2).FirstOrDefault();

        //    if (row != null)
        //    {
        //        dc.tblCourses.Remove(row);

        //        int results = dc.SaveChanges();
        //        Assert.AreNotEqual(0, results);
        //    }
        //}
    }
}
