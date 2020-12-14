using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinq.Settings;
using BlockBase.BBLinq.Tests.TestModel.Data;
using System;

namespace BlockBase.BBLinq.Tests.TestModel.DataAccess
{
    public class TestModelContext : BbContext
    {
        public TestModelContext() : base(new BbSettings() { NodeAddress = "http://40.121.160.216/nodedb1", DatabaseName = "testingApps", UserAccount = "sandbox", PrivateKey = "5HzL18MQEMChpGsaEok364FdsQnjWHMS8yK76X7NvpPHLdZTsao" })
        {
        }

        public BbSet<Country, int> Countries { get; set; }
        public BbSet<Team, int> Teams { get; set; }
        public BbSet<Player, Guid> Players { get; set; }
        public BbSet<GameRun, Guid> Runs { get; set; }
        public BbSet<Tournament, int> Tournaments{ get; set; }
        public BbSet<Publisher, Guid> Publishers { get; set; }
        public BbSet<Game, Guid> Games { get; set; }
        public BbSet<Developer, Guid> Developers{ get; set; }
    }
}
