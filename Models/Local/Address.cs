using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Local
{
    public class LocalAddress
    {
        [NotMapped]
        public List<LessonStructure> Subjects { get; set; }
        public string AddressLine1 { get; set; }
        public string Street { get; set; }
        public string Area { get; set; }
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
        public bool IsLearningLocation { get; set; }
        public int TeacherId { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; }
    }
}
