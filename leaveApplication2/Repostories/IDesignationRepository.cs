using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IDesignationRepository
    {
        Task<IReadOnlyCollection<Designation>> GetDesignationsAsync();
    }
}