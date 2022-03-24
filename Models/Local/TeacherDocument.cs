namespace IEduZimAPI.Models.Local
{
    public class TeacherDocument
    {
        public int QualificationDocumentsId { get; set; }
        public string FileUrl { get; set; }
        public bool Authenticated { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
