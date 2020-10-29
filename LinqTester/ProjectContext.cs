using BBLinq.Context;
using BBLinq.Data;

namespace LinqTester
{
    public class ProjectContext : BBContext
    {
        public ProjectContext():base("http://40.121.160.216/nodedb1", "railwaytest"){ }

        public BBSet<trainStation> TrainStations { get; set; }

    }
}
