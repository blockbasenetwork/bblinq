using BlockBase.BBLinq.Tests.TestModel.Data;
using BlockBase.BBLinq.Tests.TestModel.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockBase.BBLinq.Tests
{
    [TestClass]
    public class DeleteTest
    {
        private Country _simpleRecord = new Country() { Id = 1, Name = "Portugal" };
        private Country _nonExistingRecord = new Country() { Id = 8, Name = "Portugal" };
        private string _countryName = "England";
        private string _countryNameFirstHalf = "Fra";
        private string _countryNameSecondHalf = "nce";

        [TestMethod]
        public void AllDelete()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Delete();
            Assert.IsTrue(res.Result.Succeeded);
        }

        [TestMethod]
        public void DeleteOnObject()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Delete(_simpleRecord);
            Assert.IsTrue(res.Result.Succeeded);
        }

        [TestMethod]
        public void DeleteNonExisting()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Delete(_nonExistingRecord);
            Assert.IsFalse(res.Result.Succeeded);
        }

        [TestMethod]
        public void DeleteOnCondition()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Where(c => c.Name == "England").Delete(_simpleRecord);
            Assert.IsTrue(res.Result.Succeeded);
        }

        [TestMethod]
        public void DeleteOnConditionWithOperation()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Where(c => c.Name == "Eng"+"land").Delete(_simpleRecord);
            Assert.IsTrue(res.Result.Succeeded);
        }

        [TestMethod]
        public void DeleteOnConditionWithVariable()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Where(c => c.Name == _countryName).Delete(_simpleRecord);
            Assert.IsTrue(res.Result.Succeeded);
        }

    }
}
