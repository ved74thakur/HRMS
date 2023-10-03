using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class EmployeeLeaveRepository : IEmployeeLeaveRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeLeaveRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<EmployeeLeave>> GetAllEmployeesLeaveAsync()
        {
            return await _context.EmployeeLeaves.ToListAsync();
            //return await _context.Employees.Include(e => e.Designation).AsNoTracking().ToListAsync();
        }



        public async Task<EmployeeLeave> CreateEmployeeLeaveAsync(EmployeeLeave leave)
        {
            _context.EmployeeLeaves.Add(leave);
            await _context.SaveChangesAsync();
            return leave;
        }

        public async Task<EmployeeLeave> GetEmployeeLeaveByIdAsync(long id)
        {
            var singleEmployeeLeave = await _context.EmployeeLeaves.FindAsync(id);
            if (singleEmployeeLeave == null)
            {
                return null;
            }
            return singleEmployeeLeave;
        }

        public async Task<IReadOnlyCollection<EmployeeLeave>> GetEmployeeLeaveByEmployeeId(long employeeId)
        {
            var employeeLeaves = await _context.EmployeeLeaves
        .Where(e => e.employeeId == employeeId)
        .ToListAsync();

            return employeeLeaves.AsReadOnly();
        }
    }
}
