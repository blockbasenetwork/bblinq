using System.Linq;
using BlockBase.BBLinqTests.TestData.DataAccess.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockBase.BBLinqTests
{


    [TestClass]
    public class InsertQueryTests
    {
        [TestMethod]
        public void InsertRegular()
        {
            using var ctx = new RailwayContext();
            
            var customers = ctx.Customers.SelectAsync().Result;
            Assert.IsTrue(customers.Any());
        }

        [TestMethod]
        public void TestSelectDynamic()
        {
            using var ctx = new RailwayContext();
            var customers = ctx.Customers.SelectAsync(x => new {Nome = x.Name, TemPasse = x.WithPass}).Result;
            Assert.IsTrue(customers.Any());
        }


        [TestMethod]
        public void TestSelectNew()
        {
            using var ctx = new RailwayContext();
        }
    }
}
