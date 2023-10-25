using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class ApplicationPageRepository : IApplicationPageRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationPageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationPages> CreateApplicationPage(ApplicationPages page)
        {
            try
            {
                _context.ApplicationPages.Add(page);
                await _context.SaveChangesAsync();
                return page;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<ApplicationPages> UpdateApplicationPage(ApplicationPages page)
        {
            try
            {
                _context.ApplicationPages.Update(page);
                await _context.SaveChangesAsync();
                return page;
            }
            catch (Exception ex)
            {

                throw ex; // Rethrow the exception to propagate it up the call stack
            }
        }


        public ApplicationPages GetById(int id)
        {
            return _context.ApplicationPages.FirstOrDefault(r => r.Id == id);
        }

        public async Task<IReadOnlyCollection<ApplicationPages>> GetApplicationPagesAsync()
        {
            return await _context.ApplicationPages.ToListAsync();
        }

        public async Task DeleteApplicationPageAsync(int id)
        {
            var role = await _context.ApplicationPages.FindAsync(id);
            if (role != null)
            {
                _context.ApplicationPages.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
