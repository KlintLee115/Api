using System.ComponentModel.DataAnnotations;

namespace Api.Model.People
{
    public class Account(string name, string email, string password, string phoneNumber)
    {
        [Key]
        public Guid Id { get; init; }

        [EmailAddress]
        public string Email { get; set; } = email;

        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; } = name;

        [StringLength(20, MinimumLength = 1)]
        public string Password { get; private set; } = password;

        public string PhoneNumber { get; set; } = phoneNumber;

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string ProfilePic { get; set; } = string.Empty;
    }
}