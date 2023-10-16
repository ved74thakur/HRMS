using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly ApplicationDbContext _context;
        public GenderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<Gender>> GetGendersAsync()
        {
            try
            {
                var designations = await _context.Genders.ToListAsync();
                return designations;
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log the error or throw a custom exception
                // You can also return an empty list or null if appropriate for your use case
                throw; // Re-throw the exception if you want to propagate it to the caller
            }
        }
       
    }
}
