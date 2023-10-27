using leaveApplication2.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace leaveApplication2.Dtos
{
    public class EmployeeRoleDTO
    {
        public Employee Employee { get; set; }
        //public List<RoleAssign> RoleAssigns { get; set; }
        public List<UserRoleMapping> UserRoleMappings { get; set; } // Add this property



    }
}
