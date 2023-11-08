using leaveApplication2.Dtos;

namespace leaveApplication2.Services
{
    public interface IAuthService
    {
        Task<LoginDetailDto> AuthenticateUser(string username, string password);
    }
}
