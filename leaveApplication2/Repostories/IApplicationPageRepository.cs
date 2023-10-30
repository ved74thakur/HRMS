using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IApplicationPageRepository
    {
        Task<ApplicationPage> CreateApplicationPage(ApplicationPage page);
        ApplicationPage GetById(int id);
        Task<IReadOnlyCollection<ApplicationPage>> GetApplicationPagesAsync();
        Task<ApplicationPage> UpdateApplicationPage(ApplicationPage page);
        Task DeleteApplicationPageAsync(int id);
    }
}
