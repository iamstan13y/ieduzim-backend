namespace IEduZimAPI.Models.Local
{
    public class Payment
    {
        public string UserId { get; set; }
        public string Payer { get; set; }
        public string PaymentMethod { get; set; }
        public string AccountNumber { get; set; }
        public string Status { get; set; }
        public string PollUrl { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
