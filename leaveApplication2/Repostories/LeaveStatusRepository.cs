using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class LeaveStatusRepository : ILeaveStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<LeaveStatus>> GetLeaveStatusesAsync()
        {
            return await _context.LeaveStatuses.ToListAsync();
        }
       
        public async Task<LeaveStatus> GetLeaveStatusByIdAsync(int leaveStatusId)
        {
            return await _context.LeaveStatuses.FindAsync(leaveStatusId);
        }

        //GettingLeaveStatusByCode
        public async Task<LeaveStatus> GetLeaveStatusByCodeAsync(string leaveStatusNameCode)
        {
            return await _context.LeaveStatuses
                .SingleOrDefaultAsync(ls => ls.leaveStatusNameCode == leaveStatusNameCode);
        }




        public async Task<LeaveStatus> CreateLeaveStatusAsync(LeaveStatus leaveStatus)
        {
            _context.LeaveStatuses.Add(leaveStatus);
            await _context.SaveChangesAsync();
            return leaveStatus;
        }



        public async Task<LeaveStatus> UpdateLeaveStatusAsync(LeaveStatus leaveStatus)
        {
            _context.LeaveStatuses.Update(leaveStatus);
            await _context.SaveChangesAsync();
            return leaveStatus;
        }


        /*
        public async Task DeleteLeaveStatusAsync(int leaveStatusId)
        {
            var leaveStatus = await _context.LeaveStatuses.FindAsync(leaveStatusId);
            if (leaveStatus != null)
            {
                _context.LeaveStatuses.Remove(leaveStatus);
                await _context.SaveChangesAsync();
            }
        }
        */
    }
}
