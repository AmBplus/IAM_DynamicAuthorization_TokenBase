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
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        
        public DbSet<AspIdentityUserToken> AspIdentityUserTokens { get; set; }
        public DbSet<UserTokenEntity> UserTokens { get; set; }
        public DbSet<UserClaimEntity> UserClaims { get; set; }
        public DbSet<RoleClaimEntity> RoleClaims { get; }
        public DbSet<SystemEntity> SystemEntities { get; set; }
        public DbSet<UserProfileEntity> UsersProfiles { get; set; }
        public DbSet<GroupPermissionEntity> GroupPermissions { get; set; }

        public DbSet<MenuEntity> MenuEntities { get; set; }
        public DbSet<RoleMenuEntity> RoleMenuEntities { get; set; }
        public DbSet<MenuGroupEntity> MenuGroupEntities { get; set; }
        
    }
}
