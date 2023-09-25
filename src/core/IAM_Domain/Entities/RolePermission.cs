namespace AccessManagement.Entities;

public class ApplicationRolePermission
{
    public Guid Id { get; set; }
    public ApplicationDisplayName DisplayName { get; set; }
    public List<ApplicationRole> applicationRoles { get; set; }
}
