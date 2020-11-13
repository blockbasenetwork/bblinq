using BlockBase.BBLinq.DataAnnotations;

namespace BlockBase.BBLinqTests.TestData.Data
{
    [Table("trainStation")]
    public class TrainStation
    {
        [PrimaryKey]
        [Field("id")]
        public int Id { get; set; }

        [ForeignKey("trains")]
        [Field("train_id")]
        public int? TrainId { get; set; }

        [ForeignKey("stations")]
        [Field("station_id")]
        public int? StationId { get; set; }

        [ForeignKey("costumers")]
        [Field("costumer_id")]
        public int? CostumerId { get; set; }

    }
}
