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
        public AccessManagementDbContext(DbContextOptions<AccessManagementDbContext> options) :base(options) { }    
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationPermission> Permissions { get; set; }
        public DbSet<ApplicationDisplayName> DisplayNames { get; set; }
        public DbSet<ApplicationUserToken> UserTokens { get; set; }
        public DbSet<ApplicationUserClaim> UserClaims { get; set; }
        public DbSet<ApplicationRoleClaim> RoleClaims { get; }
        public DbSet<ApplicationSmsCode> SmsCodes { get; set; }
        public DbSet<ApplicationProfile> UsersProfiles { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<PermissionOperationType> PermissionOperationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        
           
            base.OnModelCreating(builder);
        }
    }
}
