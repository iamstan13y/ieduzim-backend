using System;
using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class AddressSearchRequest
    {
        public string UserId { get; set; }
        public int SubjectId { get; set; }
        public List<int> LessonDays { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}