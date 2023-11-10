using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public class FinancialYearService : IFinancialYearService
    {
        private readonly IFinancialYearRepository _financialYearRepository;
        public FinancialYearService(IFinancialYearRepository financialYearRepository)
        {
            _financialYearRepository = financialYearRepository;
        }

        public async Task<IEnumerable<FinancialYear>> GetFinancialYearsAsync()
        {
            var financialYears =  await _financialYearRepository.GetFinancialYearsAsync();
            if (financialYears == null )
            {
                return null;
            }
            return financialYears;
        }
        public async Task<FinancialYear> CreateFinancialYearAsync(FinancialYear financialYear)
        {
            if (financialYear.startDate > financialYear.endDate)
            {
                // Handle the validation error, e.g., by throwing an exception or returning an error message.
                throw new ArgumentException("Start date cannot be greater than end date.");
            }
            return await _financialYearRepository.CreateFinancialYearAsync(financialYear);
        }

        public async Task<FinancialYear> GetFinancialYearByIdAsync(int id)
        {
            return await _financialYearRepository.GetFinancialYearByIdAsync(id);
        }

        public async Task<FinancialYear> DeleteFinancialYearAsync(int financialYearId)
        {
            return await _financialYearRepository.DeleteFinancialYearAsync(financialYearId);
        }

        public async Task<FinancialYear> UpdateFinancialYearAsync(int financialYearId)
        {
            return await _financialYearRepository.UpdateFinancialYearAsync(financialYearId);
        }

        public async Task<IEnumerable<FinancialYear>> GetActiveFinancialYearsAsync(Expression<Func<FinancialYear, bool>> filter)
        {
            
            return await _financialYearRepository.GetActiveFinancialYearsAsync(filter);
        }


    }
}
