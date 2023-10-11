using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(Employee employee);
    }
}
