﻿namespace AccessManagement.Entities
{
    public class MenuGroupEntity : BaseEntity<int>
    {
        public MenuGroupEntity() { 
            SubMenus = new List<MenuGroupEntity>(); 
        }
        public MenuGroupEntity Parent { get; set; }
        public List<MenuGroupEntity> SubMenus { get; set; }
        
        public string Name { get; set; }    
         
    }
}