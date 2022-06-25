using IEduZimAPI.Models.Data;
using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class HubSearchResponse
    {
        public Hub Hub { get; set; }
        public Subject Subject { get; set; }
        public List<HubLessonSchedule> LessonSchedules {get; set;}
    }
}