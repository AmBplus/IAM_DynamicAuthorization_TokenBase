namespace AccessManagement.Entities
{
    public class UserPermissionEntity : BaseEntity<string>
    {
        public string Name { get; set; }    

        public string PhoneNumber { get; set; }
        public string Email { get; set; }   
        
    }
}
