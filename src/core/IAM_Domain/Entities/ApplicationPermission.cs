using IAM_Domain.Entities;

namespace AccessManagement.Entities
{
    public class ApplicationPermission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MaduleName { get; set;}
        public string GroupName { get; set;}
        public string ActionName { get; set; }
        public GroupPermission GroupPermission { get; set; }
        public List<PermissionOperationType> PermissionOperation { get; set; }
    }
}
