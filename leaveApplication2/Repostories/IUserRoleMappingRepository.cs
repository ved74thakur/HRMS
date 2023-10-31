using System.Linq.Expressions;
using leaveApplication2.Dtos;
using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IUserRoleMappingRepository
    {
        //Task<IEnumerable<UserRoleMapping>> CreateUserRoleMapping(IEnumerable<UserRoleMapping> mappings);
        //Task<UserRoleMapping> CreateUserRoleMappingAsync(UserRoleMapping mapping);
        Task<List<UserRoleMappingDTO>> CreateUserRoleMappings(List<UserRoleMappingDTO> mappings);

        Task<UserRoleMapping> GetUserRoleMappingById(int id);

        Task<IReadOnlyCollection<UserRoleMapping>> GetUserRoleMappingsAsync();
        Task<UserRoleMapping> UpdateUserRoleMapping(UserRoleMapping mapping);
        Task<UserRoleMapping> DeleteUserRoleMappingById(int id);
        Task<IReadOnlyCollection<UserRoleMapping>> GetRoleAssignIDbyUserRoleMapping(Expression<Func<UserRoleMapping, bool>> filter);

    }
}
