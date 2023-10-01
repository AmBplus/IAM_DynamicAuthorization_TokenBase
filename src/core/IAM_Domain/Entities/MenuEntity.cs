using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Entities
{
    public class MenuEntity : BaseEntity<int>
    {
        public MenuEntity() { Children = new List<MenuEntity>(); }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }   

        public string Url { get; set; }
        
        public MenuEntity? Parent { get; set; }
        public int? ParentId { get; set; }
        public List<MenuEntity>? Children { get; set; }
        public MenuGroupEntity MenuGroup { get; set; }
    }
}
