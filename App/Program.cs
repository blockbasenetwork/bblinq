using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BlockBase.BBLinq.Annotations;
using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.Parsers;
using BlockBase.BBLinq.Pocos.ExpressionParser;
using BlockBase.BBLinq.Queries.BlockBase;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinq.Settings;

namespace App
{

    [Table("Players")]
    public class Player
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [Column("GamerTag")]
        public string Name { get; set; }

        public int? PlayerCode { get; set; }

        public virtual List<Score> Scores { get; set; }
    }

    [Table("Games")]
    public class Game
    {
        [PrimaryKey]
        public int Id { get; set; }

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

        [Encrypted]
        public int Value { get; set; }

        [NotMapped]
        public int ScoreHex { get; set; }
    }

    public class ScoreSystemContext : BlockBaseContext
    {

        public ScoreSystemContext(BlockBaseSettings settings) : base(settings)
        {
        }

        public ScoreSystemContext() : base("address", "ScoreSystem", "account", "abc")
        {
        }

        public BbSet<Player> Players { get; set; }
        public BbSet<Game> Games { get; set; }
        public BbSet<Score> Scores { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var gameA = new Game() {Id = 1, Name = "A"};
            //var gameB = new Game() {Id = 2, Name = "B" };
            //var gameC = new Game() {Id = 3, Name = "C" };
            //var gameD = new Game() {Id = 4, Name = "D" };
            //int num = 8;
            //var insQuery = new InsertQuery<Game>(new List<Game>() {gameA, gameB, gameC, gameD});
            //var delQuery = new DeleteQuery<Game>((p) => p.Id == 3);
            var delQuery =
                new CreateDatabaseQuery("testingDb", new Type[] {typeof(Player), typeof(Score), typeof(Game)});
            var st = delQuery.GenerateQuery();
        }

        static ExpressionNode ParseExp(Expression<Func<Game, bool>> exp)
        {
           return ExpressionParser.ParseExpression(exp);
        }
    }
}
