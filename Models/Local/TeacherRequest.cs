﻿namespace IEduZimAPI.Models.Local
{
    public class TeacherRequest
    {
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public int TitleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string PhysicalAddress { get; set; }
        public string EducationalQualification { get; set; }
        public string QualificationUrl { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Occupation { get; set; }
        public string BankAccount { get; set; }
        public string City { get; set; }
    }
}