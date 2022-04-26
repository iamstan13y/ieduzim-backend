using IEduZimAPI.Models.Enums;

namespace IEduZimAPI.Models.Local
{
    public class SubscriptionRequest
    {
        public string UserId { get; set; }
        public int SubjectId { get; set; }
        public int Amount { get; set; }
        public string PhoneNumber { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int PaymentPeriod { get; set; }
    }
}