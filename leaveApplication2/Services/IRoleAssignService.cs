using System.Threading.Tasks;
using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IRoleAssignService
    {
        Task<IReadOnlyCollection<RoleAssign>> GetRoleAssignsAsync();
        Task<RoleAssign> CreateRoleAssign(RoleAssign role);
        Task<RoleAssign> UpdateRoleAssign(RoleAssign role);
        Task DeleteRoleAssignAsync(int id);



    }
}
