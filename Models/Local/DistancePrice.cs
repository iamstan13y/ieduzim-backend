namespace IEduZimAPI.Models.Local
{
    public class DistancePrice
    {
        public int DistanceLowerRange { get; set; }
        public int DistanceUpperRange { get; set; }
        public double Price { get; set; }
        public int CurrencyId { get; set; }
    }
}
