using System;
using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class LessonRequest
    {
        public List<int> SubscriptionIds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Confirmed { get; set; }
    }
}