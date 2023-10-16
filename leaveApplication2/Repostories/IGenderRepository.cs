using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IGenderRepository
    {
        Task<IReadOnlyCollection<Gender>> GetGendersAsync();
    }
}