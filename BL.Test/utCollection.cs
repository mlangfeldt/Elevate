using BL;
using BL.Models;

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

    }
}
