using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.DataAnnotations;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Pocos;
using BlockBase.BBLinq.Pocos.Nodes;
using BlockBase.BBLinq.Queries.RecordQueries;
using BlockBase.BBLinq.Queries.StructureQueries;
using BlockBase.BBLinq.Settings;
using Org.BouncyCastle.Asn1.Crmf;

namespace App
{

    public enum PlayerType
    {
        Casual,
        Hardcore
    }

    [Table("Players")]
    public class Player
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [Column("GamerTag")]
        public string Name { get; set; }

        public int? PlayerCode { get; set; }
        public PlayerType PlayerType { get; set; }
        public virtual List<Score> Scores { get; set; }
    }

    [Table("Games")]
    public class Game
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public virtual List<Score> Scores { get; set; }

    }

    [Table("Scores")]
    public class Score
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(Player))]
        public Guid PlayerId { get; set; }

        [ForeignKey(typeof(Game))]
        public int GameId { get; set; }

        [Range(1965, 2012, 4)]
        public DateTime BirthDate { get; set; }

        [EncryptedValue(3)]
        public int Value { get; set; }

        [NotMapped]
        public int ScoreHex { get; set; }
    }

    //public class ScoreSystemContext : BlockBaseContext
    //{

    //    public ScoreSystemContext(BlockBaseSettings settings) : base(settings)
    //    {
    //    }

    //    public ScoreSystemContext() : base("address", "ScoreSystem", "account", "abc")
    //    {
    //    }

    //    public BbSet<Player> Players { get; set; }
    //    public BbSet<Game> Games { get; set; }
    //    public BbSet<Score> Scores { get; set; }
    //}
    

    class Program
    {
        static void Main(string[] args)
        {
            var i = new bool[8];
            i[3] = true;
            int u = 8;
            int r = 4;

            var createQuery = new CreateDatabaseQuery("dba", new[] {typeof(Player), typeof(Game), typeof(Score)});
            var str = createQuery.GenerateQueryString();

            //var del = new BlockBaseDeleteRecordQuery<Player>((p => 

            //    !p.IsTrue && p.Age == 3 && p.Name == u.ToString()));
            //var str = del.GenerateQueryString();
        }
    }
}
