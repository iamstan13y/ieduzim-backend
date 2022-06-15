﻿using System;

namespace IEduZimAPI.Models.Local
{
    public class SubjectRequest
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public int CurrencyId { get; set; }
        public int LevelId { get; set; }
        public int LessonLocationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}