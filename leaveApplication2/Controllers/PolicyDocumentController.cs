using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyDocumentController : ControllerBase
    {
        private readonly IPolicyDocumentService _policyDocumentService;

        public PolicyDocumentController(IPolicyDocumentService policyDocumentService)
        {
            _policyDocumentService = policyDocumentService;
        }

        [HttpGet("documents")]
        public async Task<ActionResult<IReadOnlyCollection<PolicyDocument>>> GetPolicyDocuments()
        {
            var policyDocuments = await _policyDocumentService.GetPolicyDocumentsAsync();
            return Ok(policyDocuments);
        }

        [HttpGet("document/{id}")]
        public async Task<ActionResult<PolicyDocument>> GetPolicyDocumentById(int id)
        {
            var policyDocument = await _policyDocumentService.GetPolicyDocumentByIdAsync(id);

            if (policyDocument == null)
            {
                return NotFound();
            }

            return Ok(policyDocument);
        }

        [HttpPost("upload")]
        public async Task<ActionResult<int>> UploadPolicyDocument([FromForm] PolicyDocumentUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var documentId = await _policyDocumentService.UploadPolicyDocumentAsync(model.PolicyName, model.File, model.IsActive);

            return CreatedAtAction(nameof(GetPolicyDocumentById), new { id = documentId }, documentId);
        }
    }
}

