using leaveApplication2.Data;
using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class PolicyDocumentRepository : IPolicyDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public PolicyDocumentRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<IReadOnlyCollection<PolicyDocument>> GetPolicyDocumentsAsync()
        {
            return await _context.PolicyDocuments.ToListAsync();

        }

        public async Task<PolicyDocument> GetPolicyDocumentByIdAsync(int id)
        {
            var document = await _context.PolicyDocuments.FindAsync(id);
            return document;

        }


        public async Task<int> UploadPolicyDocument(PolicyDocument policyDocument)
        {
            _context.PolicyDocuments.Add(policyDocument);
            await _context.SaveChangesAsync();
            return policyDocument.policyDocumentId;
        }
            


    }
}
