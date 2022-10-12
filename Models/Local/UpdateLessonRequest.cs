using IEduZimAPI.Models.Enums;
using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class UpdateLessonRequest
    {
        public List<int> SubscriptionIds { get; set; }
        public int LessonId { get; set; }
        public LessonStatus LessonStatus { get; set; }
    }
}