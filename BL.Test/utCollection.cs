using BL;
using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.BL.Test
{
    [TestClass]
    public class utCollection
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, CollectionManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(1, CollectionManager.LoadById(1).Id);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Collection collection = CollectionManager.LoadById(3);
            collection.UserId = 2;
            Assert.IsTrue(CollectionManager.Update(collection, true) > 0);
        }
    }
}
