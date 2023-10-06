namespace AccessManagement.Entities;

public class RolePermissionEntity
{
    public RolePermissionEntity() { 
    Roles = new List<RoleEntity>();   
    }
    public Guid Id { get; set; }
    public PermissionEntity Permission { get; set; }
    public int PermissionId { get; set; }
   
    public List<RoleEntity> Roles { get; set; }
}
