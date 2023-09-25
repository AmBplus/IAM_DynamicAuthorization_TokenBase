using Microsoft.AspNetCore.Identity;

namespace AccessManagement.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public List<ApplicationPermission> Permissions { get; set; }     
    }
}
