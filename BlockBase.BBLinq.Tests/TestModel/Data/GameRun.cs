using BlockBase.BBLinq.Annotations;
using System;

namespace BlockBase.BBLinq.Tests.TestModel.Data
{
    [Table(Name ="gameRuns")]
    public class GameRun
    {
        [PrimaryKey, Field(Name ="gameRunId")]
        public Guid Id { get; set; }
    
        [Field(Name ="duration")]
        public TimeSpan Duration { get; set; }

        [Field(Name = "score")]
        public int Score { get; set; }

        [Field(Name = "runDate")]
        public DateTime RunDate { get; set; }

        [ForeignKey(Name ="players"), Field(Name = "playerId")]
        public Guid PlayerId { get; set; }

        [ForeignKey(Name ="games"), Field(Name = "gameId")]
        public Guid GameId { get; set; }

        [ForeignKey(Name = "tournaments"), Field(Name = "tournamentId")]
        public Guid TournamentId { get; set; }
    }
}
