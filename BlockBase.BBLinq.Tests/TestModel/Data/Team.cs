using BlockBase.BBLinq.Annotations;

namespace BlockBase.BBLinq.Tests.TestModel.Data
{
    [Table(Name="teams")]
    public class Team
    {
        [PrimaryKey, Field(Name ="teamId")]
        public int Id { get; set; }

        [Field(Name = "name")]
        public string Name { get; set; }

        [Field(Name = "website")]
        public int Website { get; set; }

        [ForeignKey(Name = "countries"), Field(Name = "countryId")]
        public int CountryId { get; set; }
    }
}
