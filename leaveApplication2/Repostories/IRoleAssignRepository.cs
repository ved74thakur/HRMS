using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IRoleAssignRepository
    {
        Task<RoleAssign> CreateRoleAssign(RoleAssign role);
        RoleAssign GetById(int id);
        Task<IReadOnlyCollection<RoleAssign>> GetRoleAssignsAsync();
        Task<RoleAssign> UpdateRoleAssign(RoleAssign role);

        //Task<RoleAssign> UpdateRoleAssignAsync(RoleAssign roleAssign);

         Task DeleteRoleAssignAsync(int id);
    }
}
