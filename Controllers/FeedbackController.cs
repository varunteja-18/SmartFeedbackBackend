using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smart_feedback_api.Data;
using smart_feedback_api.Models;

namespace smart_feedback_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] FeedbackDto dto)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(dto.Category) || string.IsNullOrWhiteSpace(dto.Message))
            {
                return BadRequest("Category and message are required.");
            }

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr == null || !int.TryParse(userIdStr, out int userId))
            {
                return Unauthorized("Invalid user ID.");
            }

            var feedback = new Feedback
            {
                Category = dto.Category,
                Message = dto.Message,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Feedback submitted successfully" });
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetMine()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr == null || !int.TryParse(userIdStr, out int userId))
            {
                return Unauthorized("Invalid user ID.");
            }

            var feedbacks = await _context.Feedbacks
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            return Ok(feedbacks);
        }
    }
}
