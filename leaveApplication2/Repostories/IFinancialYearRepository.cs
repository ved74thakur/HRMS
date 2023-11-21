using leaveApplication2.Models.leaveApplication2.Models;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public interface IFinancialYearRepository
    {
        Task<IReadOnlyCollection<FinancialYear>> GetFinancialYearsAsync();
        Task<IReadOnlyCollection<FinancialYear>> GetFinancialYearsAsync(Expression<Func<FinancialYear, bool>> filter);

        Task<FinancialYear> CreateFinancialYearAsync(FinancialYear financialYear);

        Task<FinancialYear> GetFinancialYearByIdAsync(int id);
        Task<FinancialYear> GetFinancialYearByIdAsync(Expression<Func<FinancialYear, bool>> filter);
        Task<FinancialYear> DeleteFinancialYearAsync(int financialYearId);

        Task<FinancialYear> UpdateFinancialYearAsync(int financialYearId);

       
    }
}
