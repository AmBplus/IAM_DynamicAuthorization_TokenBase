using AccessManagement.Entities;
using IAM_Domain.Entities;
using AccessManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Data
{
    public class AccessManagementDbContext : 
        IdentityDbContext<UserEntity, 
        RoleEntity, Guid, UserClaimEntity
        , UserRoleEntity, UserLoginEntity,
        RoleClaimEntity, AspIdentityUserToken>
        , IAccessManagementDbContext
    {
        public AccessManagementDbContext(DbContextOptions<AccessManagementDbContext> options) :base(options) { }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }

        public DbSet<AspIdentityUserToken> AspIdentityUserTokens { get; set; }
        public DbSet<UserTokenEntity> UserTokens { get; set; }
        public DbSet<UserClaimEntity> UserClaims { get; set; }
        public DbSet<RoleClaimEntity> RoleClaims { get; }
        
        public DbSet<UserProfileEntity> UsersProfiles { get; set; }
        public DbSet<GroupPermissionEntity> GroupPermissions { get; set; }

        public DbSet<MenuEntity> MenuEntities { get; set; }
        public DbSet<RoleMenuEntity> RoleMenuEntities { get; set; }
        public DbSet<MenuGroupEntity> MenuGroupEntities { get; set; }
        public DbSet<SystemEntity> SystemEntities { get ; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
