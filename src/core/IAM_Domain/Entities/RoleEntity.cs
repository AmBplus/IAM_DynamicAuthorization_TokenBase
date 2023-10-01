using Microsoft.AspNetCore.Identity;

namespace AccessManagement.Entities
{
    public class RoleEntity : IdentityRole<Guid>
    {
        public List<PermissionEntity> Permissions { get; set; }     
    }
}
