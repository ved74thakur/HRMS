using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IUserRoleMappingServices
    {
        Task<IEnumerable<UserRoleMapping>> GetUserRoleMappingsAsync();
        Task<UserRoleMapping> CreateUserRoleMappingAsync(UserRoleMapping mapping);
        Task<UserRoleMapping> GetUserRoleMappingByIdAsync(int id);
        Task<UserRoleMapping> UpdateUserRoleMappingAsync(int id, UserRoleMapping mapping);
        Task DeleteUserRoleMappingAsync(int id);
    }
}
