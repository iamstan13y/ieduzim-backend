using IEduZimAPI.Models.Enums;

namespace IEduZimAPI.Models.Local
{
    public class SubscriptionRequest
    {
        public string UserId { get; set; }
        public int LessonStructureId { get; set; }
        public double Amount { get; set; }
        public string PhoneNumber { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int PaymentPeriod { get; set; }
    }
}