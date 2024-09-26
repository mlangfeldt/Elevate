using BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.BL.Test
{
    [TestClass]
    public class utCourse
    {
        [TestMethod]
        public void InsertTest()
        {
            Course course = new Course
            {
                Name = "test",
                Description = "test"
            };

            Assert.AreEqual(1, CourseManager.Insert(course, true));
        }

        //[TestMethod()]
        //public void LoadTest()
        //{
        //    Assert.AreEqual(3, CourseManager.Load().Count);
        //}

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(1, CourseManager.LoadById(1).Id);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Course course = CourseManager.LoadById(3);
            course.Name = "test";
            course.Description = "test";
            Assert.IsTrue(CourseManager.Update(course, true) > 0);
        }
        [TestMethod]
        public void DeleteTest()
        {
            Assert.AreEqual(1, CourseManager.Delete(2, true));
        }
    }
}
