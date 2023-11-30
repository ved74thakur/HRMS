namespace leaveApplication2.Dtos
{
    public class PolicyDocumentUploadModel
    {
        public string PolicyName { get; set; }
        public IFormFile File { get; set; }
        public bool IsActive { get; set; }
    }
}
