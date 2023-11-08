using leaveApplication2.Models.leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IFinancialYearRepository
    {
        Task<IReadOnlyCollection<FinancialYear>> GetFinancialYearsAsync();
        Task<FinancialYear> CreateFinancialYearAsync(FinancialYear financialYear);

        Task<FinancialYear> GetFinancialYearByIdAsync(int id);
        Task<FinancialYear> DeleteFinancialYearAsync(int financialYearId);

        Task<FinancialYear> UpdateFinancialYearAsync(int financialYearId);
    }
}
