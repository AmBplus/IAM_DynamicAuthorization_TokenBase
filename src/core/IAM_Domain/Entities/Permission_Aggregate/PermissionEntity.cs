
namespace AccessManagement.Entities
{
    public class PermissionEntity : BaseEntity<int>
    {

        public bool IsEnabled { get; set; } = true;
        public string Name { get; set; }
        
        public string ActionName { get; set; }
        
        
        public GroupPermissionEntity GroupPermission { get; set; }
        public int GroupPermissionId { get; set; }

        public SystemEntity? System { get; set; }
        public int? SystemId { get; set; }
        public List<UserEntity> Users { get; set; }
        public List<RoleEntity> Roles { get; set; }  
    }
}
