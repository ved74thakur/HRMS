using leaveApplication2.Data;
using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace leaveApplication2.Repostories
{
    public class FinancialYearRepository : IFinancialYearRepository
    {
        private readonly ApplicationDbContext _context;

        public FinancialYearRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<IReadOnlyCollection<FinancialYear>> GetFinancialYearsAsync()
        {
            return await _context.FinancialYears.ToListAsync();
            
        }



        //create financial Year
        public async Task<FinancialYear> CreateFinancialYearAsync(FinancialYear financialYear)
        {
            
            if (financialYear.startDate > financialYear.endDate)
            {
           
                throw new InvalidOperationException("Start date cannot be greater than end date.");
            }
         


            _context.FinancialYears.Add(financialYear);
            await _context.SaveChangesAsync();
            return financialYear;
        }


        public async Task<FinancialYear> GetFinancialYearByIdAsync(int id)
        {
            var singleFinancialYear = await _context.FinancialYears.FindAsync(id);
            return singleFinancialYear;

        }

        public async Task<FinancialYear> DeleteFinancialYearAsync(int financialYearId)
        {
            var financialYear = await _context.FinancialYears.FindAsync(financialYearId);
            if (financialYear != null)
            {
                _context.FinancialYears.Remove(financialYear);
                await _context.SaveChangesAsync();
            }
            return financialYear;
        }
        public async Task<FinancialYear> UpdateFinancialYearAsync(int financialYearId)
        {
            var existingFinancialYear = await _context.FinancialYears.FindAsync(financialYearId);

            if (existingFinancialYear == null)
            {
                
                throw new Exception("Financial year not found.");
            }

            //existingFinancialYear.startDate = financialYear.startDate;
            //existingFinancialYear.endDate = financialYear.endDate;
            existingFinancialYear.ActiveYear = false;
            _context.Update(existingFinancialYear);
            // Save the changes to the database.
            await _context.SaveChangesAsync();

            return existingFinancialYear;
        }
        public async Task<IReadOnlyCollection<FinancialYear>> GetFinancialYearsAsync(Expression<Func<FinancialYear, bool>> filter)
        {
            return await _context.FinancialYears.Where(filter).ToListAsync();
        }

        //public async Task<IReadOnlyCollection<FinancialYear>> GetFinancialYearsAsync(Expression<Func<FinancialYear, bool>> filter)
        //{
        //    return await _context.FinancialYears.AsNoTracking().Where(filter).ToListAsync();
        //}

        public async Task<FinancialYear> GetFinancialYearByIdAsync(Expression<Func<FinancialYear, bool>> filter)
        {
            return await _context.FinancialYears.AsNoTracking().Where(filter).FirstOrDefaultAsync(); 
        }

    }
}
