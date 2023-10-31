using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IApplicationPageRepository
    {
        Task<ApplicationPages> CreateApplicationPage(ApplicationPages page);
        ApplicationPages GetById(int id);
        Task<IReadOnlyCollection<ApplicationPages>> GetApplicationPagesAsync();
        Task<ApplicationPages> UpdateApplicationPage(ApplicationPages page);
        Task DeleteApplicationPageAsync(int id);
    }
}
