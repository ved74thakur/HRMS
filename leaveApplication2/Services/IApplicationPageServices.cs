using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IApplicationPageServices
    {

        Task<IEnumerable<ApplicationPages>> GetApplicationPagesAsync();
        Task<ApplicationPages> CreateApplicationPageAsync(ApplicationPages page);
        Task<ApplicationPages> GetApplicationPageByIdAsync(int id);
        Task<ApplicationPages> UpdateApplicationPageAsync(int id, ApplicationPages page);
        Task DeleteApplicationPageAsync(int id);
    }
}
