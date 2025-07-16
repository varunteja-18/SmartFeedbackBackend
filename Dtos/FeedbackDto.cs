using System.ComponentModel.DataAnnotations;

public class FeedbackDto
{
    [Required]
    public string Category { get; set; }
    [Required]
    public string Message { get; set; }
}
