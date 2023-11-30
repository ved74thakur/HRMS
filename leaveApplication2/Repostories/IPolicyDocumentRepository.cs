using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IPolicyDocumentRepository
    {
        Task<IReadOnlyCollection<PolicyDocument>> GetPolicyDocumentsAsync();

        Task<PolicyDocument> GetPolicyDocumentByIdAsync(int id);

        Task<int> UploadPolicyDocument(PolicyDocument policyDocument);


    }
}
