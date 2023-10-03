using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class LeaveTypeRepository: ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<LeaveType>> GetAllLeaveTypesAsync()
        {
            return await _context.LeaveTypes.ToListAsync();
        }

        public async Task<LeaveType> GetLeaveTypeByIdAsync(int id)
        {
            return await _context.LeaveTypes.FirstOrDefaultAsync(lt => lt.leaveTypeId == id);
        }

        public async Task CreateLeaveTypeAsync(LeaveType leaveType)
        {
            _context.LeaveTypes.Add(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLeaveTypeAsync(LeaveType leaveType)
        {
            _context.Entry(leaveType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLeaveTypeAsync(int id)
        {
            var leaveTypeToDelete = await _context.LeaveTypes.FindAsync(id);
            if (leaveTypeToDelete != null)
            {
                _context.LeaveTypes.Remove(leaveTypeToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
