using System;
using BlockBase.BBLinq.DataAnnotations;

namespace BlockBase.BBLinqTests.TestData.Data
{
    [Table("trains")]
    public class Train
    {
        [PrimaryKey]
        [Field("id")]
        public int Id { get; set; }
        
        [Field("trainCode")]
        public string TrainCode { get; set; }
        
        [Field("trainLine")]
        public string TrainLine { get; set; }
        
        [Field("startsAt ")]
        public int StartsAt { get; set; }
        
        public int EndsAt { get; set; }
    }
}
