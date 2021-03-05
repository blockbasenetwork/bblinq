using System;
using System.Collections.Generic;
using BlockBase.BBLinq.DataAnnotations;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Queries.RecordQueries;
using BlockBase.BBLinq.Sets;

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
        public bool IsTrue { get; set; }
        public int Age { get; set; }
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

    public class I
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(Player))]
        public Guid PlayerId { get; set; }
    }

    public class A
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(Player))]
        public Guid PlayerId { get; set; }
    }

    public class D
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(Game))]
        public Guid GameId { get; set; }
    }

    public class B
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(I))]
        public Guid IId { get; set; }
    }
    public class C
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(B))]
        public Guid BId { get; set; }
    }
    public class E
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(B))]
        public Guid BId { get; set; }
    }
    public class G
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(B))]
        public Guid BId { get; set; }
    }
    public class F
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(D))]
        public Guid DId { get; set; }
    }

    public class H
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(D))]
        public Guid DId { get; set; }
    }

    public class X
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(D))]
        public Guid DId { get; set; }
    }

    public class Z
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(D))]
        public Guid DId { get; set; }
    }

    public class Y
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(D))]
        public Guid DId { get; set; }
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
            var set = new BlockBaseSet<Player>();
            var join = set.Join<Score>().Join<Game>().Join<A>().Join<I>().Join<B>().Join<D>().Join<E>().Join<F>().Join<G>()
                .Join<H>().Join<C>().Join<X>().Join<Y>().Join<Z>().Take(3).Skip(8).Encrypt().Select<>();

            //var i = new bool[8];
            //i[3] = true;
            //int u = 8;
            //int r = 4;
            //var player1 = new Player()
            //    {Id = Guid.NewGuid(), Name = "Fernando", PlayerCode = 1, PlayerType = PlayerType.Hardcore};
            //var player2 = new Player()
            //    { Id = Guid.NewGuid(), Name = "André", PlayerCode = 2, PlayerType = PlayerType.Casual };
            //var player3 = new Player()
            //    { Id = Guid.NewGuid(), Name = "João", PlayerCode = 3, PlayerType = PlayerType.Casual };
            //var player4 = new Player()
            //    { Id = Guid.NewGuid(), Name = "Tiago", PlayerCode = 4, PlayerType = PlayerType.Hardcore };
            ////var createQuery = new CreateDatabaseQuery("dba", new[] {typeof(Player), typeof(Game), typeof(Score)});
            ////var str = createQuery.GenerateQueryString();

            ////var upQuery = new BlockBaseUpdateRecordQuery<Player>(player, (p)=>p.Name=="F");
            ////var str = upQuery.GenerateQueryString();

            //var res = u + r * 3;
            ////var del = new BlockBaseDeleteRecordQuery<Player>((p => !p.IsTrue && p.Age == res && p.Name == u.ToString()));
            //var del = new BlockBaseInsertRecordQuery<Player>(new List<Player>() {player1, player2, player3, player4});
            //var str = del.GenerateQueryString();
        }
    }
}
