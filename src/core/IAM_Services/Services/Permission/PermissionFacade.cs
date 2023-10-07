using AccessManagement.Services.Permission.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Facade
{
    public interface  IPermissionFacade
    {
        
        public HasRolePermissionCommandHandler HasRolePermission { get; }
    }
}
