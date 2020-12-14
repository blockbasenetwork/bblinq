using BlockBase.BBLinq.Tests.TestModel.Data;
using BlockBase.BBLinq.Tests.TestModel.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BlockBase.BBLinq.Tests
{
    [TestClass]
    public class SelectTest
    {
        private Country _simpleRecord = new Country() { Id = 1, Name = "Portugal" };
        private string _countryName = "England";

        [TestMethod]
        public void SimpleList()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.List().Result;
            Assert.IsTrue(res.Succeeded && res.Result.ToList().Count > 0);
        }

        [TestMethod]
        public void SimpleGet()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Get(_simpleRecord.Id).Result;
            Assert.IsTrue(res.Succeeded && res.Result.Name == "England");
        }

        [TestMethod]
        public void ListWithCondition()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Where(c => c.Id<4).List().Result;
            Assert.IsTrue(res.Succeeded && res.Result.ToList().Count > 0);
        }

        [TestMethod]
        public void ListWithConditionOperation()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Where(c => c.Name == "Engl"+"and").List().Result;
            Assert.IsTrue(res.Succeeded && res.Result.ToList().Count > 0);
        }

        [TestMethod]
        public void ListWithConditionVariable()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Where(c => c.Name == _countryName).List().Result;
            Assert.IsTrue(res.Succeeded && res.Result.ToList().Count > 0);
        }

        [TestMethod]
        public void ListWithLimit()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Limit(1).List().Result;
            Assert.IsTrue(res.Succeeded && res.Result.ToList().Count == 1 && res.Result.ToList()[0].Name == "England");
        }

        [TestMethod]
        public void ListWithOffset()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Limit(1).Offset(2).List().Result;
            Assert.IsTrue(res.Succeeded && res.Result.ToList().Count == 1 && res.Result.ToList()[0].Id == 2);
        }

        [TestMethod]
        public void JoinOnTwo()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Join<Team>().List((c,t)=>new { CountryName = c.Name, TeamName = t.Name, TeamWebsite = t.Website}).Result;
            Assert.IsTrue(res.Succeeded && res.Result.ToList().Count == 3);
        }

        [TestMethod]
        public void JoinOnThree()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Join<Team>().Join<Player>().List((c,t, p)=>new {PlayerId = p.Id, CountryName = c.Name, TeamName = t.Name, TeamWebsite = t.Website, PlayerName = p.Name, Email = p.Email, BirthDate = p.BirthDate}).Result;
            Assert.IsTrue(res.Succeeded && res.Result.ToList().Count == 11);
        }

        [TestMethod]
        public void JoinOnThreeWithObject()
        {
            using var ctx = new TestModelContext();
            var res = ctx.Countries.Join<Team>().Join<Player>().List((c, t, p) => new PlayerData(){ PlayerId = p.Id, CountryName = c.Name, TeamName = t.Name, TeamWebsite = t.Website, PlayerName = p.Name, Email = p.Email, BirthDate = p.BirthDate }).Result;
            Assert.IsTrue(res.Succeeded && res.Result.ToList().Count == 11);
        }

        //To be done

        [TestMethod]
        public void JoinOnAll()
        {

        }
    }
}
