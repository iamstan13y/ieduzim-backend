using IEduZimAPI.Models.Enums;
using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class SubscriptionRequest
    {
        public string UserId { get; set; }
        public List<int> LessonScheduleIds { get; set; }
        public double Amount { get; set; }
        public string PhoneNumber { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}