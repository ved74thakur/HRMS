using leaveApplication2.Dtos;
using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IUserRoleMappingServices
    {
        Task<IEnumerable<UserRoleMappingDTO>> GetUserRoleMappingsAsync();
        //Task<UserRoleMapping> CreateUserRoleMappingAsync(UserRoleMapping mapping);
        Task<List<UserRoleMappingDTO>> CreateUserRoleMappings(List<UserRoleMappingDTO> mappings);

        Task<UserRoleMapping> GetUserRoleMappingByIdAsync(int id);
        Task<UserRoleMapping> UpdateUserRoleMappingAsync(int id, UserRoleMapping mapping);
        Task DeleteUserRoleMappingAsync(int id);
    }
}
