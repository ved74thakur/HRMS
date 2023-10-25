using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public class UserRoleMappingRepository : IUserRoleMappingRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRoleMappingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<UserRoleMapping>> GetUserRoleMappingsAsync()
        {
            return await _context.UserRoleMappings.ToListAsync();
        }

        public async Task<IReadOnlyCollection<UserRoleMapping>> GetUserRoleMappingsAsync(Expression<Func<UserRoleMapping, bool>> filter)
        {
            return await _context.UserRoleMappings.Where(filter).ToListAsync();
        }

        public async Task<UserRoleMapping> CreateUserRoleMapping(UserRoleMapping mapping)
        {
            try
            {
               _context.UserRoleMappings.Add(mapping);
                await _context.SaveChangesAsync();
                return mapping; // Return the UserRoleMapping object, not a Task<UserRoleMapping>
            }
            catch (Exception ex)
            {
                // Handle the exception here, log it, or take appropriate action.
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

        public async Task<UserRoleMapping> GetUserRoleMappingById(int id)
        {
            var mapping = await _context.UserRoleMappings.FindAsync(id);
            return mapping;
        }

        public async Task<UserRoleMapping> UpdateUserRoleMapping(UserRoleMapping mapping)
        {
            try
            {
                _context.UserRoleMappings.Update(mapping);
                await _context.SaveChangesAsync();
                return mapping;
            }
            catch (Exception ex)
            {
                // Handle the exception here, log it, or take appropriate action.
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

        public async Task<UserRoleMapping> DeleteUserRoleMappingById(int id)
        {
            var mapping = await _context.UserRoleMappings.FindAsync(id);
            if (mapping == null)
            {
                return null;
            }

            //_context.UserRoleMappings.Remove(mapping);
            await _context.SaveChangesAsync();
            return mapping;
        }

        public async Task<IReadOnlyCollection<UserRoleMapping>> GetFilteredUserRoleMappingsAsync(
            Expression<Func<UserRoleMapping, bool>> filter = null,
            Expression<Func<UserRoleMapping, object>> orderBy = null)
        {
            IQueryable<UserRoleMapping> query = _context.UserRoleMappings;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            return await query.ToListAsync();
        }
    }
}
