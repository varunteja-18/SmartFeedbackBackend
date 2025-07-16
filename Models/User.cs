using System.ComponentModel.DataAnnotations;

namespace smart_feedback_api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = "User";
    }
}
