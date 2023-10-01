using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Entities
{
    public class RoleMenuEntity : BaseEntity<int>
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public Guid RoleId { get; set; }
        public RoleEntity Role { get; set; }
        public MenuEntity MenuEntity { get; set; }
        public int MenuEntityId { get; set; }
    }
}
