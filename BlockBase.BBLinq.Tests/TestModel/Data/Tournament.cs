using BlockBase.BBLinq.Annotations;

namespace BlockBase.BBLinq.Tests.TestModel.Data
{
    [Table(Name="tournaments")]
    public class Tournament
    {
        [PrimaryKey, Field(Name ="tournamentId")]
        public int Id { get; set; }

        [Field(Name = "name")]
        public string Name { get; set; }

        [Field(Name = "description")]
        public string Description { get; set; }

        [Field(Name = "prize"), Range(Buckets =4, Maximum =10000, Minimum=100)]
        public string Prize { get; set; }

        [Field(Name = "year")]
        public string Year { get; set; }

        [Encrypted(Buckets =4), Field(Name = "qrCode")]
        public string QrRegisterCode { get; set; }
        
    }
}
