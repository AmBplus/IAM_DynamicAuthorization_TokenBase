using Microsoft.AspNetCore.Identity;

namespace AccessManagement.Entities
{
    public class RoleEntity : IdentityRole<Guid>
    {
        public RoleEntity()
        {
            Permissions = new List<PermissionEntity>();
        }
        public List<PermissionEntity> Permissions { get; set; }     

    }
}
