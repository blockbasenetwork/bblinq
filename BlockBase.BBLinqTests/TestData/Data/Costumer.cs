using BlockBase.BBLinq.DataAnnotations;

namespace BlockBase.BBLinqTests.TestData.Data
{
    [Table("costumers")]
    public class Customer
    {
        [PrimaryKey]
        [Field("id")]
        public int Id { get; set; }
        
        [Field("name")]
        public string Name { get; set; }
        
        [Field("withPass")]
        public bool WithPass { get; set; }
        
    }
}
