using BlockBase.BBLinq.Tests.TestModel.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockBase.BBLinq.Tests
{
    [TestClass]
    public class StructureTests
    {
        [TestMethod]
        public void CreateDatabase()
        {
            using var tc = new TestModelContext();
            var res = tc.CreateDatabase().Result;
            Assert.IsTrue(res.Succeeded);
        }

        [TestMethod]
        public void DropDatabase()
        {
            using var tc = new TestModelContext();
            var res = tc.DropDatabase().Result;
            Assert.IsTrue(res.Succeeded);
        }
    }
}
