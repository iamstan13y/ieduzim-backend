﻿using IEduZimAPI.Models.Enums;
using System;

namespace IEduZimAPI.Models.Data
{
    public class HubLessonSchedule
    {
        public int Id { get; set; }
        public Day LessonDay { get; set; }
        public int SubjectId { get; set; }
        public int HubId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}