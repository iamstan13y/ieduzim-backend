namespace IEduZimAPI.Models.Local
{
    public class Subscription
    {
        public string Title { get; set; }
        public int Amount { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentMethod { get; set; }
        public string Payer { get; set; }
        public string UserId { get; set; }
        public string PaymentPeriod { get; set; }
        public string Currency { get; set; }
    }
}
