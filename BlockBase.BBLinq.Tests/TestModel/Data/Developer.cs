using BlockBase.BBLinq.Annotations;
using System;

namespace BlockBase.BBLinq.Tests.TestModel.Data
{
    [Table(Name = "developers")]
    public class Developer
    {
        [PrimaryKey, Field(Name = "developerId")]
        public Guid Id { get; set; }

        [Field(Name = "name")]
        public string Name { get; set; }

        [Field(Name = "active")]
        public bool Active { get; set; }

        [Field(Name = "vatNumber")]
        public long VatNumber { get; set; }

        [Field(Name = "website")]
        public string Website { get; set; }
        
        [ForeignKey(Name ="countries"), Field(Name="countryId")]
        public int CountryId { get; set; }
    }
}
