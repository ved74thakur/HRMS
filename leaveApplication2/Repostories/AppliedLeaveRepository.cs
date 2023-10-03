using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class AppliedLeaveRepository : IAppliedLeaveRepository
    {
        private readonly ApplicationDbContext _context;

        public AppliedLeaveRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<AppliedLeave>> GetAppliedLeavesAsync()
        {
            return await _context.AppliedLeaves.ToListAsync();
            //return await _context.AppliedLeaves.Include(e => e.LeaveStatus).AsNoTracking().ToListAsync();
            
            //return await _context.EmployeeLeaves.Include(e => e.LeaveType).AsNoTracking().ToListAsync();
        }

        public async Task<AppliedLeave> CreateAppliedLeave(AppliedLeave leave)
        {
            _context.AppliedLeaves.Add(leave);
             await _context.SaveChangesAsync();
            return leave;
            
        }

        public async Task<AppliedLeave> GetAppliedLeaveByIdAsync(long id)
        {
            var singleLeave = await _context.AppliedLeaves.FindAsync(id);
            if (singleLeave == null)
            {
                return null;
            }
            return singleLeave;

        }
        
        
        


        public async Task<AppliedLeave> UpdateAppliedLeaveAsync(long id,  AppliedLeave leave)
        {
            var singleLeave = await _context.AppliedLeaves.FindAsync(id);
            if (singleLeave == null)
            {
                return null;
            }
            singleLeave.leaveTypeId = leave.leaveTypeId;
            singleLeave.StartDate = leave.StartDate;
            singleLeave.EndDate = leave.EndDate;
            singleLeave.LeaveReason = leave.LeaveReason;
            singleLeave.applyLeaveDay = leave.applyLeaveDay;
            singleLeave.remaingLeave = leave.remaingLeave;
            singleLeave.balanceLeave = leave.balanceLeave;

            await _context.SaveChangesAsync();
            return singleLeave;

        }

        public async Task<AppliedLeave> DeleteAppliedLeaveByIdAsync(long id)
        {
            var leave = await _context.AppliedLeaves.FindAsync(id);
            if (leave == null)
            {
                return leave;
            }
            
            _context.AppliedLeaves.Remove(leave);
            await _context.SaveChangesAsync();
            return leave;

            
        }

    }
}
