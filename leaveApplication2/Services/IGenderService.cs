using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IGenderService
    {
        Task<IReadOnlyCollection<Gender>> GetGendersAsync();
    }
}
