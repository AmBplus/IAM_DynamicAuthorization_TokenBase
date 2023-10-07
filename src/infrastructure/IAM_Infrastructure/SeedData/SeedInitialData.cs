using AccessManagement.Entities;
using AccessManagement.Services;
using AccessManagement.Services.Permission.Command;
using AccessManagement.Services.System.Command;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.SeedData
{
    public class SeedInitialData
    {
        public SeedInitialData(UserManager<UserEntity> userManager
            ,RoleManager<RoleEntity> roleManager, IMediator mediator
            ,ISeedControllerPermission seedControllerPermission
            )
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Mediator = mediator;
            SeedControllerPermission = seedControllerPermission;
        }

        public UserManager<UserEntity> UserManager { get; }
        public RoleManager<RoleEntity> RoleManager { get; }
        public IMediator Mediator { get; }
        public ISeedControllerPermission SeedControllerPermission { get; }

        public async Task Initial()
        {
            SeedControllerData  seedControllerData = SeedControllerPermission.GetInfo();
            
            

            
            
            try
            {
                
                    await AddPermissionForAddPermissionToRole(seedControllerData.nameSpace, seedControllerData.GroupPermission, seedControllerData.Actions);
                
            }
            catch (Exception e)
            {

                throw;
            }

            try
            {
                await AddSeedRoles();
            }
            catch (Exception e)
            {

                throw;
            }



            try
            {
               await AddSeedUserAddToRole();
            }
            catch (Exception e)
            {

                throw;
            }
            try
            {
                await AddAdminAndProgrammerRoleToPermission(seedControllerData.nameSpace, seedControllerData.GroupPermission, seedControllerData.Actions);
            }
            catch (Exception e)
            {

                throw;
            }

            

            #region Local Function
            async Task AddPermissionForAddPermissionToRole(string nameSpace, string groupPermission, List<string> actions)
            {
                //AddPermission For AddPermissionToRole
                await Mediator.Send(new AddSystemCommandRequest
                (
                    Name: nameSpace
                ));
                await Mediator.Send(new AddGroupPermissionCommandRequest(groupPermission));
                foreach (var item in actions)
                {
                    await Mediator.Send(new AddPermissionCommandRequest(nameSpace, groupPermission, item));
                }

            }

            async Task AddSeedRoles()
            {
                var createAdminRoleResult = await RoleManager.CreateAsync(new RoleEntity
                {
                    Name = nameof(UserRolesConst.Admin),
                    Id = UserRolesConst.Admin,
                });
                if (!createAdminRoleResult.Succeeded)
                {
                    throw new Exception("اضافه کردن نقش ادمین با خطا مواجه شد");
                }
                var createProgrammerRoleResult = await RoleManager.CreateAsync(new RoleEntity
                {
                    Name = nameof(UserRolesConst.Programmer),
                    Id = UserRolesConst.Programmer,
                });
                if (!createProgrammerRoleResult.Succeeded)
                {
                    throw new Exception("اضافه کردن نقش برنامه نویس با خطا مواجه شد");
                }
                var createSimpleUserRoleResult = await RoleManager.CreateAsync(new RoleEntity
                {
                    Name = nameof(UserRolesConst.SimpleUser),
                    Id = UserRolesConst.SimpleUser,
                });
                if (!createSimpleUserRoleResult.Succeeded)
                {
                    throw new Exception("اضافه کردن کاربر ساده با خطا مواجه شد");
                }
            }

            async Task AddSeedUserAddToRole()
            {
                // Add Programmer
                try
                {
                    #region Add Programmer
                    var programmerUser = new UserEntity
                    {

                        Id = Guid.Parse("23EB5A2E-8D61-4FBE-AA8C-2C475EAE396C"),
                        IsActive = true,
                        IsBanned = false,
                        Email = "Programmer@Host",
                        UserName = "Programmer@Host"
                    };
                    var resultCreateProgrammerUser = await UserManager.CreateAsync(programmerUser, "123@mM!K");
                    if (resultCreateProgrammerUser.Succeeded)
                    {
                        var programmerRole = await RoleManager.FindByNameAsync(nameof(UserRolesConst.Programmer));

                        if (programmerRole != null)
                        {
                            IdentityResult roleResult = await UserManager.AddToRoleAsync(programmerUser, programmerRole.Name);
                        }
                    }
                    #endregion

                }
                catch (Exception e)
                {

                    throw;
                }
                // Add Admin
               try {
                    #region Add Admin
                    var AdminUser = new UserEntity
                    {

                        Id = Guid.Parse("8AE7377E-3E81-4323-B633-DAC06B8E46EB"),
                        IsActive = true,
                        IsBanned = false,
                        Email = "Admin@Host",
                        UserName = "Admin@Host"
                    };
                    var resultCreateAdminUser = await UserManager.CreateAsync(AdminUser, "123@mM!K");
                    if (resultCreateAdminUser.Succeeded)
                    {
                        var AdminRole = await RoleManager.FindByNameAsync(nameof(UserRolesConst.Admin));

                        if (AdminRole != null)
                        {
                            IdentityResult roleResult = await UserManager.AddToRoleAsync(AdminUser, AdminRole.Name);
                        }
                    }

                    #endregion
                }
                catch (Exception e) {
                    throw;
                }
                // Add SimpleUser
                try
                {

                    #region SimpleUser
                    var SimpleUser = new UserEntity
                    {

                        Id = Guid.Parse("560CD920-792A-4546-919C-2BCAF3C283E3"),
                        IsActive = true,
                        IsBanned = false,
                        Email = "SimpleUser@Host",
                        UserName = "SimpleUser@Host"
                    };
                    var resultCreateSimpleUser = await UserManager.CreateAsync(SimpleUser, "123@mM!K");
                    if (resultCreateSimpleUser.Succeeded)
                    {
                        var SimpleUserRole = await RoleManager.FindByNameAsync(nameof(UserRolesConst.SimpleUser));

                        if (SimpleUserRole != null)
                        {
                            IdentityResult roleResult = await UserManager.AddToRoleAsync(SimpleUser, SimpleUserRole.Name);
                        }
                    }
                    #endregion
                }
                catch (Exception e)
                {

                    throw;
                }
            }

            async Task AddAdminAndProgrammerRoleToPermission(string nameSpace, string groupPermission, List<string> actionPermission)
            {
                foreach (string action in actionPermission)
                {
                    string permissionName = $"{nameSpace}:{groupPermission}:{action}";
                    await Mediator.Send(new AddPermissionToRoleCommandRequest
                    {
                        PermissionName = permissionName,
                        RoleName = nameof(UserRolesConst.Programmer)
                    });
                    await Mediator.Send(new AddPermissionToRoleCommandRequest
                    {
                        PermissionName = permissionName,
                        RoleName = nameof(UserRolesConst.Admin)
                    });

                }
            }
            #endregion



        }
    }
}
