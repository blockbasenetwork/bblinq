using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinqTests.TestData.Data;

namespace BlockBase.BBLinqTests.TestData.DataAccess.Context
{
    public class RailwayContext : BbContext
    {
        public RailwayContext() : base("http://40.121.160.216/nodedb1", "railwaytest")
        {
        }

        public BbSet<Customer> Customers { get; set; }
        public BbSet<Staff> Staff { get; set; }
        public BbSet<Station> Stations { get; set; }
        public BbSet<Train> Trains { get; set; }
        public BbSet<TrainStation> TrainStations { get; set; }
    }
}
