using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IUserRoleMappingRepository
    {
        Task<UserRoleMapping> CreateUserRoleMapping(UserRoleMapping mapping);
        Task<UserRoleMapping> GetUserRoleMappingById(int id);
        Task<IReadOnlyCollection<UserRoleMapping>> GetUserRoleMappingsAsync();
        Task<UserRoleMapping> UpdateUserRoleMapping(UserRoleMapping mapping);
        Task<UserRoleMapping> DeleteUserRoleMappingById(int id);
    }
}
