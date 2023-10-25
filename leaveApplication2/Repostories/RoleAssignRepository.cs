using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class RoleAssignRepository: IRoleAssignRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleAssignRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RoleAssign> CreateRoleAssign(RoleAssign role)
        {
            try
            {
                _context.RoleAssigns.Add(role);
                await _context.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<RoleAssign> UpdateRoleAssign(RoleAssign role)
        {
            try
            {
                _context.RoleAssigns.Update(role);
                await _context.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it or take appropriate action
                // For example, you can rethrow the exception, return a default value, or handle it gracefully
                // Logging the exception is a good practice to help with debugging
                // Example: _logger.LogError(ex, "An error occurred while updating the applied leave.");

                throw ex; // Rethrow the exception to propagate it up the call stack
            }
        }


        public RoleAssign GetById(int id)
        {
            return _context.RoleAssigns.FirstOrDefault(r => r.RoleAssignId == id);
        }

        public async Task<IReadOnlyCollection<RoleAssign>> GetRoleAssignsAsync()
        {
            return await _context.RoleAssigns.ToListAsync();
        }

        public async Task DeleteRoleAssignAsync(int id) // Use 'int' instead of 'long'
        {
            var roleAssign = await _context.RoleAssigns.FindAsync(id);
            if (roleAssign != null)
            {
                _context.RoleAssigns.Remove(roleAssign);
                await _context.SaveChangesAsync();
            }
        }
    }
}
