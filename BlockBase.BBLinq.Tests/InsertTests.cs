using BlockBase.BBLinq.Tests.TestModel.Data;
using BlockBase.BBLinq.Tests.TestModel.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockBase.BBLinq.Tests
{
    [TestClass]
    public class InsertTests
    {
        private Country _simpleRecord = new Country() { Id = 1, Name = "Spain" };
        private string _countryName = "England";
        private string _countryNameFirstHalf = "Fra";
        private string _countryNameSecondHalf = "nce";

        [TestMethod]
        public void SimpleInsert()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Insert(_simpleRecord);
            Assert.IsTrue(res.Result.Succeeded);
        }

        [TestMethod]
        public void ExistingInsert()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Insert(new Country() { Id = 4, Name = "Spain" });
            Assert.IsFalse(res.Result.Succeeded);
        }

        [TestMethod]
        public void InsertWithValuesOnVariables()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Insert(new Country() { Id = 2, Name = _countryName });
            Assert.IsTrue(res.Result.Succeeded);
        }

        [TestMethod]
        public void InsertWithValuesOnOperations()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Insert(new Country() { Id = 3, Name = _countryNameFirstHalf + _countryNameSecondHalf });
            Assert.IsTrue(res.Result.Succeeded);
        }
    }
}
