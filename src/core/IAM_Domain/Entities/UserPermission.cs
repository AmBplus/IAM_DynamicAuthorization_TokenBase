namespace AccessManagement.Entities
{
    public class ApplicationUserPermission
    {
        public string Id { get; set; }
        public string Name { get; set; }    

        public string PhoneNumber { get; set; }
        public string Email { get; set; }   
        public List<ApplicationPermissionLevelAccess> PermissionLevels { get; set; }   
        
    }
   public class ApplicationPermission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MaduleName { get; set;}
        public string GroupName { get; set;}
        public string ActionName { get; set; }
        public ApplicationPermissionLevelAccess PermissionLevelAccess { get; set; }
    }
}
