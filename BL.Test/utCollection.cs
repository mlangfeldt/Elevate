using Elevate.BL;
using Elevate.BL.Models;

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

        [TestMethod]
        public void InsertTest()
        {
            Collection collection = new Collection
            {
                CourseId = 1,
                UserId = 1
            };

            Assert.AreEqual(4, CollectionManager.Insert(collection, true));
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
            collection.CourseId = 2;
            collection.UserId = 2;
            Assert.IsTrue(CollectionManager.Update(collection, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Assert.AreEqual(1, CollectionManager.Delete(2, true));
        }
    }
}
