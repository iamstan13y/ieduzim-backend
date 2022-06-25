using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class HubSubjectRequest
    {
        public int HubId { get; set; }
        public int SubjectId { get; set; }
        public List<int> LessonDays { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}