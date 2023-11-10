using leaveApplication2.Data;
using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public class LeaveAllocationRepository: ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public LeaveAllocationRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsAsync()
        {
            return await _context.LeaveAllocations.AsNoTracking().ToListAsync();
        }
        public async Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsAsync(Expression<Func<LeaveAllocation, bool>> filter)
        {
            

            return await _context.LeaveAllocations.Where(filter).ToListAsync();



        }
        public async Task<LeaveAllocation> GetLeaveAllocationAsync(long id)
        {
            var leaveAllocation = await _context.LeaveAllocations.FindAsync(id);
            if (leaveAllocation == null)
            {
                return null;
            }
            return leaveAllocation;
        }
        public async Task<LeaveAllocation> GetLeaveAllocationAsync(Expression<Func<LeaveAllocation, bool>> filter)
        {
            var leaveAllocation = await _context.LeaveAllocations.FirstOrDefaultAsync(filter);

            if (leaveAllocation == null)
            {
                return null;
            }

            return leaveAllocation;
        }

        public async Task<LeaveAllocation> CreateLeaveAllocationAsync(LeaveAllocation leaveAllocation)
        {

            _context.LeaveAllocations.Add(leaveAllocation);
            await _context.SaveChangesAsync();
            return leaveAllocation;
        }

        public async Task<LeaveAllocation> DeleteLeaveAllocationAsync(int leaveAlloctionId)
        {
            var leaveAllocation = await _context.LeaveAllocations.FindAsync(leaveAlloctionId);
            if (leaveAllocation != null)
            {
                _context.LeaveAllocations.Remove(leaveAllocation);
                await _context.SaveChangesAsync();
            }
            return leaveAllocation;
        }

        public async Task<LeaveAllocation> UpdateLeaveAllocationAsync(int leaveAlloctionId)
        {
            var leaveAllocation = await _context.LeaveAllocations.FindAsync(leaveAlloctionId);

            if (leaveAllocation == null)
            {

                throw new Exception("Leave allocation id not found.");
            }

            _context.LeaveAllocations.Update(leaveAllocation);
            await _context.SaveChangesAsync();
            return leaveAllocation;

        }

        public async Task RemoveLeaveAllocationsForFinancialYearAsync(int financialYearId)
        {
            // Implement the logic to remove leave allocations for the specified financial year
            // Example: Assuming your leave allocations are stored in a database, you might use an ORM like Entity Framework

            var leaveAllocationsToRemove = await _context.LeaveAllocations
                .Where(la => la.financialYearId == financialYearId)
                .ToListAsync();

            _context.LeaveAllocations.RemoveRange(leaveAllocationsToRemove);
            await _context.SaveChangesAsync();
        }

    }
}
