using AccessManagement.Data;
using AccessManagement.Entities;
using IAM_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Data
{
    public class AccessManagementDbContext : 
        IdentityDbContext<ApplicationUser, 
        ApplicationRole, Guid, ApplicationUserClaim
        , ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
        , IAccessManagementDbContext
    {
        public AccessManagementDbContext(DbContextOptions options ) : base( options ) { }    
        public List<ApplicationPermission> ApplicationPermissions { get; set; }
        public List<ApplicationDisplayName> DisplayNames { get; set; }
        public List<ApplicationSmsCode> SmsCodes { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<ApplicationRole> Roles { get; set; }
        public List<ApplicationUserToken> UserTokens { get; set; }
        public List<ApplicationUserClaim> UserClaims { get; set; }

        public List<ApplicationRoleClaim> RoleClaims { get; set; }
        public List<ApplicationProfile> UsersProfiles { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
        
           
            base.OnModelCreating(builder);
        }
    }
}
