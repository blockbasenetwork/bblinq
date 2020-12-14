using BlockBase.BBLinq.Tests.TestModel.Data;
using BlockBase.BBLinq.Tests.TestModel.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockBase.BBLinq.Tests
{
    [TestClass]
    public class UpdateTest
    {
        private Country _simpleRecord = new Country() { Id = 1, Name = "Portugal" };
        private Country _nonExistingRecord = new Country() { Id = 8, Name = "Portugal" };
        private string _countryName = "England";
        private string _countryNameFirstHalf = "Fra";
        private string _countryNameSecondHalf = "nce";

        [TestMethod]
        public void SimpleUpdate()
        {
            using var ctx = new TestModelContext();
            _simpleRecord.Name = "Spain";
            var res = ctx.Countries.Update(_simpleRecord);
            Assert.IsTrue(res.Result.Succeeded);
        }

        [TestMethod]
        public void NonExistingUpdate()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Update(_nonExistingRecord);
            Assert.IsFalse(res.Result.Succeeded);
        }

        [TestMethod]
        public void UpdateWithVariable()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Update(new Country() { Id = 1, Name = _countryName });
            Assert.IsTrue(res.Result.Succeeded);
        }

        [TestMethod]
        public void UpdateSeveralOnCondition()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Where(c=>c.Name == "England").Update(new Country() { Id = 3, Name = "Italy" });
            Assert.IsTrue(res.Result.Succeeded);
        }

        [TestMethod]
        public void UpdateSeveralOnConditionWithVariable()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Where(c => c.Name == _countryName).Update(new Country() { Id = 3, Name = "Italy" });
            Assert.IsTrue(res.Result.Succeeded);
        }


        [TestMethod]
        public void UpdateSeveralOnConditionWithOperation()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Where(c => c.Name == _countryNameFirstHalf+_countryNameSecondHalf).Update(new Country() { Id = 3, Name = "Italy" });
            Assert.IsTrue(res.Result.Succeeded);
        }

    }
}
