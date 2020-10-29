using BBLinq;
using BBLinq.Context;
using BBLinq.Queries;
using System;

namespace LinqTester
{
    public class TrainResult
    {
        public string CostumerName { get; set; }
        public string TrainLine { get; set; }
        public string StationName { get; set; }
        public string StaffName { get; set; }
        public bool Pass { get; set; }
        public int TrainId { get; set; }
    }

    public class trains
    {
        public int id { get; set; }
        public string trainCode { get; set; }
        public string trainLine { get; set; }
        public int startsAt { get; set; }
        public int endsAt { get; set; }
    }

    public class stations
    {
        public int id { get; set; }
        public string name { get; set; }
        public int howManyLines { get; set; }
        public bool handicapAccess { get; set; }
    }

    public class staff
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string position { get; set; }
        public int yearOfBirth { get; set; }
        public string address { get; set; }
        public string socialSecurity { get; set; }
        public int salary { get; set; }
        public int? trainId { get; set; }
    }

    public class trainStation
    {
        public int id { get; set; }
        public int train_id { get; set; }
        public int station_id { get; set; }
        public int costumer_id { get; set; }
    }
    public class costumers
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool withPass { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using(var ctx = new ProjectContext())
            {
                var r = "Azambuja";
                var res = ctx.TrainStations
                            .Join<trains>((ts, t) => ts.train_id == t.id)
                            .Join<stations>((ts, t, s) => ts.station_id == s.id)
                            .Join<costumers>((ts, t, s, c) => ts.costumer_id == c.id)
                            .Join<staff>((ts, t, s, c, sf) => t.id == sf.trainId)
                            .Where((ts, t, s, c, sf) => t.trainLine == r)
                            .SelectAsync((ts, t, s, c, sf) => new TrainResult() { CostumerName = c.name, TrainLine = t.trainLine, StationName = s.name, StaffName = sf.name, Pass = c.withPass, TrainId = t.id });

                var ress = ctx.TrainStations.InsertAsync(new trainStation() {train_id = 3, costumer_id = 1 });
            }
        }
    }
}
