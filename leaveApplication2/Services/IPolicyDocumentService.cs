using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IPolicyDocumentService
    {
        Task<IReadOnlyCollection<PolicyDocument>> GetPolicyDocumentsAsync();

        Task<PolicyDocument> GetPolicyDocumentByIdAsync(int id);

        Task<int> UploadPolicyDocumentAsync(string policyName, IFormFile file, bool isActive);

    }
}
