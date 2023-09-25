using AccessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Entities
{
    public class GroupPermission
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public List<ApplicationPermission> ApplicationPermissions { get; set; }
    }
}
