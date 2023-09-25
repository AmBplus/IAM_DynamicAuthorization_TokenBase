using AccessManagement.Entities;
using Base.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Data
{
    public interface IAccessManagementDbContext : IEfDbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationPermission> Permissions { get; set; }
        public DbSet<ApplicationDisplayName> DisplayNames { get; set; } 
        public DbSet<ApplicationUserToken> UserTokens { get; set; }
        public DbSet<ApplicationUserClaim> UserClaims { get; set; } 
        public DbSet<ApplicationRoleClaim> RoleClaims { get;}
        public DbSet<ApplicationSmsCode> SmsCodes { get; set; }     
        public DbSet<ApplicationProfile> UsersProfiles { get; set; }     
        public DbSet<GroupPermission> GroupPermissions { get; set; } 
        public DbSet<PermissionOperationType> PermissionOperationTypes { get; set; } 
    }
}
