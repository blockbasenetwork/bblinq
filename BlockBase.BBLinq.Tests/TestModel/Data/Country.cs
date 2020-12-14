using BlockBase.BBLinq.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.BBLinq.Tests.TestModel.Data
{
    [Table(Name="countries")]
    public class Country
    {
        [PrimaryKey, Field(Name ="countryId")]
        public int Id { get; set; }


        [Field(Name = "name")]
        public string Name { get; set; }
    }
}
