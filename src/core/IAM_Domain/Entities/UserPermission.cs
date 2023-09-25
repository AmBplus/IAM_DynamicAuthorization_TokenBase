namespace AccessManagement.Entities
{
    public class ApplicationUserPermission
    {
        public string Id { get; set; }
        public string Name { get; set; }    

        public string PhoneNumber { get; set; }
        public string Email { get; set; }   
        public List<PermissionOperationType> PermissionLevels { get; set; }   
        
    }
}
