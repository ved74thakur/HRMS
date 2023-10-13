using leaveApplication2.Data;
using leaveApplication2.Dtos;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace leaveApplication2.Repostories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public EmployeeRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

        public async Task<Employee> RegisterEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }




        public async Task<Employee> UpdateEmployeeRegistrationById(long id, Employee request)

        {
            var singleEmployeeRegistration = await _context.Employees.FindAsync(id);
            if (singleEmployeeRegistration == null)
            {
                return null;
            }
            singleEmployeeRegistration.firstName = request.firstName;
            singleEmployeeRegistration.lastName = request.lastName;
            singleEmployeeRegistration.employeeEmail = request.employeeEmail;
            


            await _context.SaveChangesAsync();
            return singleEmployeeRegistration;

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
        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees.SingleOrDefaultAsync(e => e.employeeEmail == email);
        }

        public async Task<Employee> EmployeeLoginAsync(Employee  employee)
        {
            return await _context.Employees.SingleOrDefaultAsync(e => e.employeeEmail == employee.employeeEmail && e.employeePassword == employee.employeePassword);
        }   



    }
}
