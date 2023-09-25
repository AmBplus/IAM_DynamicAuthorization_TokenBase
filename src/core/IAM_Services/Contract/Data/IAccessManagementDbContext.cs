using AccessManagement.Entities;
using Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Data
{
    public interface IAccessManagementDbContext : IEfDbContext
    {
        public List<ApplicationUser> Users { get; set; }
        public List<ApplicationRole> Roles { get; set; }
        public List<ApplicationPermission> ApplicationPermissions { get; set; }
        public List<ApplicationDisplayName> DisplayNames { get; set; } 
        public List<ApplicationUserToken> UserTokens { get; set; }
        public List<ApplicationUserClaim> UserClaims { get; set; } 
        public List<ApplicationRoleClaim> RoleClaims { get;}
        public List<ApplicationSmsCode> SmsCodes { get; set; }     
        public List<ApplicationProfile> UsersProfiles { get; set; }     
    }
}
