using BlockBase.BBLinq.DataAnnotations;
using BlockBase.BBLinq.DataAnnotations.DataTypes;

namespace BlockBase.BBLinqTests.TestData.Data
{
    [Table("staff")]
    public class Staff
    {
        [PrimaryKey]
        [Field("id")]
        public int Id { get; set; }
        
        [Encrypted]
        [Field("name")]
        public string Name { get; set; }

        [Encrypted]
        [Field("email")]
        public string Email{ get; set; }

        [Encrypted]
        [Field("position")]
        public int Position { get; set; }
        
        [Encrypted]
        [Field("yearOfBirth")]
        public int YearOfBirth { get; set; }

        [Encrypted]
        [Field("address")]
        public string Address { get; set; }

        [Encrypted]
        [Field("salary")]
        public int Salary { get; set; }

        [ForeignKey("Train")]
        [Field("trainId")]
        public int? TrainId { get; set; }
    }
}
