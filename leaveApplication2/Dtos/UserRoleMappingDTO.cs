namespace leaveApplication2.Dtos
{
    public class UserRoleMappingDTO
    {
        public int roleAssignId { get; set; }
        public int ApplicationPageId { get; set; }

        public string roleAssignName { get; set; }
        public string roleAssignCodeName { get; set; }

        public string pageName { get; set; }
        public string pageCode { get; set; }

        public string routePath { get; set; }
        public string menuPath { get; set; }
        public bool isMenuPage { get; set; } = true;

        public string componentName { get; set; } = string.Empty;

    }
}