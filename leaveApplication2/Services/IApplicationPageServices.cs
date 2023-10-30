using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IApplicationPageServices
    {

        Task<IEnumerable<ApplicationPage>> GetApplicationPagesAsync();
        Task<ApplicationPage> CreateApplicationPageAsync(ApplicationPage page);
        Task<ApplicationPage> GetApplicationPageByIdAsync(int id);
        Task<ApplicationPage> UpdateApplicationPageAsync(int id, ApplicationPage page);
        Task DeleteApplicationPageAsync(int id);
    }
}
