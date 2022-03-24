namespace IEduZimAPI.Models.Data
{
    public class Register
    {
        //public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Phonenumber { get; set; }
        //public string Password { get; set; }
        public Register() { }
        public Register(string email, string phone)
        {
            Email = email;
            Phonenumber = phone;
        }
    }
}
