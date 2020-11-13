using BlockBase.BBLinq.DataAnnotations;

namespace BlockBase.BBLinqTests.TestData.Data
{
    [Table("stations")]
    public class Station
    {
        [PrimaryKey]
        [Field("id")]
        public int Id { get; set; }

        [Field("name")]
        public string Name { get; set; }

        [Field("howManyLines")]
        public string HowManyLines { get; set; }

        [Field("handicapAccess")]
        public bool HandicapAccess { get; set; }
    }
}
