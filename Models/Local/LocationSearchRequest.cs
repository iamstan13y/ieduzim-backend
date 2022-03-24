using IEduZimAPI.CoreClasses;
using System.Collections.Generic;

namespace IEduZimAPI.Models.Local
{
    public class LocationSearchRequest: PageRequest
    {
        public bool AutoLoad { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public string Area { get; set; }
        public int LevelId { get; set; }
        public List<int> Subjects { get; set; }
        public int ExamType { get; set; }
        public int LessonLocation { get; set; }
    }
}
