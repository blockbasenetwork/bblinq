using BlockBase.BBLinq.Annotations;
using System;

namespace BlockBase.BBLinq.Tests.TestModel.Data
{
    [Table(Name ="players")]
    public class Player
    {
        [PrimaryKey, Field(Name ="playerId")]
        public Guid Id { get; set; }
    
        [Field(Name ="name")]
        public string Name { get; set; }

        [Field(Name = "email")]
        public string Email { get; set; }

        [Encrypted(Buckets = 4), Field(Name="birthdate")]
        public DateTime BirthDate { get; set; }

        [Field(Name = "registerDate")]
        public DateTime RegisterDate { get; set; }

        [ForeignKey(Name = "teams"), Field(Name = "teamId")]
        public int TeamId { get; set; }
    }
}
