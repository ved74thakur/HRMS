using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Services
{
    public class PolicyDocumentService : IPolicyDocumentService
    {
        private readonly IPolicyDocumentRepository _policyDocumentRepository;

        public PolicyDocumentService(IPolicyDocumentRepository policyDocumentRepository)
        {
            _policyDocumentRepository = policyDocumentRepository;
        }

        public async Task<IReadOnlyCollection<PolicyDocument>> GetPolicyDocumentsAsync()
        {
            return await _policyDocumentRepository.GetPolicyDocumentsAsync();

        }

        public async Task<PolicyDocument> GetPolicyDocumentByIdAsync(int id)
        {
            var document = await _policyDocumentRepository.GetPolicyDocumentByIdAsync(id);
            return document;

        }

        public async Task<int> UploadPolicyDocumentAsync(string policyName, IFormFile file, bool isActive)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                var policyDocument = new PolicyDocument
                {
                    policyName = policyName,
                    content = memoryStream.ToArray(),
                    isActive = isActive
                };

                return await _policyDocumentRepository.UploadPolicyDocument(policyDocument);
            }
        }       



    }
}
