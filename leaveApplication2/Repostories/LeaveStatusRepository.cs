using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public class LeaveStatusRepository: ILeaveStatusRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LeaveStatus> GetLeaveStatusAsync(Expression<Func<LeaveStatus, bool>> filter)
        {
            try
            {
                var leaveStatus = await _context.LeaveStatuses.Where(filter).FirstOrDefaultAsync();

                return leaveStatus;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IReadOnlyCollection<LeaveStatus>> GetLeaveStatusesAsync()
        {
            try
            {
                var designations = await _context.LeaveStatuses.ToListAsync();
                return designations;
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log the error or throw a custom exception
                // You can also return an empty list or null if appropriate for your use case
                throw; // Re-throw the exception if you want to propagate it to the caller
            }
        }

        public async Task<IReadOnlyCollection<LeaveStatus>> GetLeaveStatusesAsync(Expression<Func<LeaveStatus, bool>> filter)
        {
            try
            {
                var Statuses = await _context.LeaveStatuses.Where(filter).ToListAsync();

                return Statuses;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
