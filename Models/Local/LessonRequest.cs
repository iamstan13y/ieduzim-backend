using System;
using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class LessonRequest
    {
        public List<int> SubscriptionIds { get; set; }
        public DateTime LessonDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}