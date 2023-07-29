namespace Data.Models
{
    public class AdminRegisterModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? dateofBirth { get; set; }
        public string?[] Interested { get; set; }
    }
}
