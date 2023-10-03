using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Employee>> GetAllEmployeesAsync()
        {
            // return await _context.Employees.ToListAsync();
            return await _context.Employees.Include(e => e.Designation).AsNoTracking().ToListAsync();
        }

       
        public async Task<Employee> GetEmployeeByIdAsync(long id)
        {
            var singleEmployee =  await _context.Employees.FindAsync(id);
            if (singleEmployee == null) 
            {
                return null;
            }
            return singleEmployee;
        }

        

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(long id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
