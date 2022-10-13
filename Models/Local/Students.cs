namespace IEduZimAPI.Models.Local
{
    public class Students
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TitleId { get; set; }
        public string Gender { get; set; }
        public string PhysicalAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AcademicLevel { get; set; }
        public string EnrolledSchool { get; set; }
        public string City { get; set; }
        public string NextOfKin { get; set; }
        public string NextOfKinContact { get; set; }
        public int LocationId { get; set; }
    }
}