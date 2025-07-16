using smart_feedback_api.Models;

namespace smart_feedback_api.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
