namespace IEduZimAPI.Models.Data
{
    public class DistancePrices
    {
        public int Id { get; set; }
        public int DistanceLowerRange { get; set; }
        public int DistanceUpperRange { get; set; }
        public int CurrencyId { get; set; }
        public double Price { get; set; }
        public virtual Currencies Currency{get; set;}

    }
}
