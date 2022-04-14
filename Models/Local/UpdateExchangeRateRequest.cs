namespace IEduZimAPI.Models.Local
{
    public class UpdateExchangeRateRequest
    {
        public int RateId { get; set; }
        public double NewRate { get; set; }
    }
}