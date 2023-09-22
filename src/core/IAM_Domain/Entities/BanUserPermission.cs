namespace AccessManagement.Entities;

/// <summary>
/// The Users Who Banned To 
/// This Permission Even Touch Have 
/// Enough Authority To Access Specific Permission
/// </summary>
public class ApplicationBanUserPermission
{
    public Guid Id { get; set; }    
    public ApplicationPermission Permisson { get; set; }
    public List<ApplicationUser> Users { get; set; }  
}
