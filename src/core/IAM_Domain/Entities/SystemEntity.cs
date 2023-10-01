namespace AccessManagement.Entities
{
    public class SystemEntity : BaseEntity<int>
    {
        public SystemEntity() { 
        SubSystems = new List<SystemEntity> ();
        }
        
        public string Name { get; set; }    
        public SystemEntity? Parent { get; set; }
        public int? ParentId { get; set; }
        public List<SystemEntity>? SubSystems { get; set; }
    }
}