using AccessManagement.Services.Permission.Command;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Facade
{
    public class PermissionFacade : IPermissionFacade
    {
         public PermissionFacade(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        private HasRolePermissionCommandHandler _HasRolePermission;
        public HasRolePermissionCommandHandler HasRolePermission =>
            _HasRolePermission ??= ServiceProvider.GetRequiredService<HasRolePermissionCommandHandler>();

        public IServiceProvider ServiceProvider { get; }
    }
}
