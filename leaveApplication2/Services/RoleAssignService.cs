using leaveApplication2.Models;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class RoleAssignService : IRoleAssignService
    {
        private readonly IRoleAssignRepository _roleAssignRepository;

        public RoleAssignService(IRoleAssignRepository roleAssignRepository)
        {
            _roleAssignRepository = roleAssignRepository;
        }

        public async Task<IReadOnlyCollection<RoleAssign>> GetRoleAssignsAsync()
        {
            try
            {
                var roleAssigns = await _roleAssignRepository.GetRoleAssignsAsync();
                return roleAssigns;
            }
            catch (Exception ex)
            {
                throw; 
            }
        }
        public async Task<RoleAssign> CreateRoleAssign(RoleAssign role)
        {


            var createRole = await _roleAssignRepository.CreateRoleAssign(role);
            return createRole;
        }
        public async Task<RoleAssign> UpdateRoleAssign(RoleAssign role)
        {

            try
            {
                var updaterole =  _roleAssignRepository.GetById(role.RoleAssignId);
                if (updaterole == null)
                {
                    throw new ApplicationException("ID not found");
                }

                // Update the properties of getSingleEmployeeById with the values from the employee object
                updaterole.RoleAssignName = role.RoleAssignName;
                updaterole.RoleAssignCodeName = role.RoleAssignCodeName;
                updaterole.IsActive = role.IsActive;
                updaterole.IsSuperAdmin = role.IsSuperAdmin;
                //   getSingleEmployeeById.employeePassword = employee.employeePassword;

                // Save the changes to the database
                var updatedEmployee = await _roleAssignRepository.UpdateRoleAssign(updaterole);

                // Commit the transaction if everything is successful
                return updatedEmployee;
            }
            catch (Exception ex)
            {
                // Handle the error and optionally log it
                throw; // Re-throw the exception for higher-level error handling
            }
        }
        public async Task DeleteRoleAssignAsync(int id)
        {
            await _roleAssignRepository.DeleteRoleAssignAsync(id);
        }

    }
}

