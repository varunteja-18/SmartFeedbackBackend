using smart_feedback_api.Models;
namespace smart_feedback_api.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}