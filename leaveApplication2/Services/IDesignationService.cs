using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IDesignationService
    {
        Task<IReadOnlyCollection<Designation>> GetDesignationsAsync();
    }
}