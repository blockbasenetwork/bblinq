
using BlockBase.BBLinq.Annotations;
using System;

namespace BlockBase.BBLinq.Tests.TestModel.Data
{
    [Table(Name = "games")]
    public class Game
    {
        [PrimaryKey, Field(Name = "gameId")]
        public Guid Id { get; set; }

        [Field(Name = "name")]
        public string Name { get; set; }

        [Field(Name = "price")]
        public double? Price { get; set; }

        [Field(Name = "releaseDate")]
        public DateTime ReleaseDate { get; set; }

        [ForeignKey(Name="developers"), Field(Name="developerId")]
        public Guid DeveloperId { get; set; }
        
        [ForeignKey(Name="publishers"), Field(Name="publisherId")]
        public int PublisherId { get; set; }

    }
}
