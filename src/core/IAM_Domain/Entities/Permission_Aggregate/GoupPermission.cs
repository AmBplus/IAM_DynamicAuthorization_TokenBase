using AccessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Entities
{
    public class GroupPermissionEntity : BaseEntity<int>
    {
        public GroupPermissionEntity() {
            ApplicationPermissions = new List<PermissionEntity>();  
        }  
        public string Name { get; set; }
        public List<PermissionEntity> ApplicationPermissions { get; set; }
    }
}
