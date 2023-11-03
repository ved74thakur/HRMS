using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        //public async Task<IReadOnlyCollection<AppliedLeave>> GetAppliedLeavesAsync(Expression<Func<AppliedLeave, bool>> filter)
        //{
        //    return await _context.AppliedLeaves.Where(filter).ToListAsync();
        //}
        public async Task<IReadOnlyCollection<AppliedLeave>> GetAppliedLeavesAsync(Expression<Func<AppliedLeave, bool>> filter)
        {
            try
            {
                return await _context.AppliedLeaves
                    .Where(filter)
                    .Include(appliedLeave => appliedLeave.Employee)
                    .Include(appliedLeave => appliedLeave.LeaveType)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception here
                // You can rethrow the exception or return an empty collection, depending on your requirements
                // For logging, you can use a logging library or simply write to the console
                Console.WriteLine($"An error occurred: {ex.Message}");
                // You may want to rethrow the exception to let the caller know about it
                throw;
            }
        }



        public async Task<AppliedLeave> CreateAppliedLeave(AppliedLeave leave)
        {
            try
            {
                _context.AppliedLeaves.Add(leave);
                await _context.SaveChangesAsync();
                return leave;
            }
            catch (Exception ex)
            {
                // Handle the exception here or log it for debugging
                // You can also throw a custom exception if needed
                // Example: throw new CustomException("Failed to create applied leave.", ex);

                // You can also rethrow the exception if you want to propagate it up the call stack
                throw;
            }

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
        public async Task<AppliedLeave> UpdateAppliedLeaveAsync(AppliedLeave leave)
        {
            try
            {
                _context.AppliedLeaves.Update(leave);
                await _context.SaveChangesAsync();
                return leave;
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it or take appropriate action
                // For example, you can rethrow the exception, return a default value, or handle it gracefully
                // Logging the exception is a good practice to help with debugging
                // Example: _logger.LogError(ex, "An error occurred while updating the applied leave.");

                throw; // Rethrow the exception to propagate it up the call stack
            }
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

        public async Task<IReadOnlyCollection<AppliedLeave>> GetFilteredLeavesAsync(
     Expression<Func<AppliedLeave, bool>> filter = null,
     Expression<Func<AppliedLeave, object>> orderBy = null)
        {
            IQueryable<AppliedLeave> query = _context.AppliedLeaves;

            try
            {
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    query = query.OrderBy(orderBy);
                }

                // Capture the generated SQL query as a string
               // string sqlQuery = query.ToQueryString();

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception or log it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception if necessary
            }
        }

    //    public async Task<IReadOnlyCollection<AppliedLeave>> GetUnApprovedAppliedLeavesAsync(AppliedLeave appliedLeave)
    //    {
    //        // Replace the condition with your specific criteria
    //        var unapprovedLeaves = await _context.AppliedLeaves
    //.Where(leave => leave.employeeId == appliedLeave.employeeId && leave.IsApproved == appliedLeave.IsApproved && leave.leaveTypeId == appliedLeave.leaveTypeId && leave.IsHalfDay == appliedLeave.IsHalfDay && leave.IsRejected == appliedLeave.IsRejected)
    //.ToListAsync();

    //        return unapprovedLeaves;
    //    }
        public async Task<IReadOnlyCollection<AppliedLeave>> GetUnApprovedAppliedLeavesAsync(AppliedLeave appliedLeave)
        {
            // Replace the condition with your specific criteria
            var unapprovedLeaves = await _context.AppliedLeaves
    .Where(leave => leave.employeeId == appliedLeave.employeeId && leave.IsApproved == appliedLeave.IsApproved && appliedLeave.IsRejected == true && appliedLeave.IsApproved == false)
    .ToListAsync();

            return unapprovedLeaves;
        }
    }





}
