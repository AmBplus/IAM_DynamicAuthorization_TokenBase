using Microsoft.AspNetCore.Identity;

namespace AccessManagement.Entities;

public class UserTokenEntity : IdentityUserToken<Guid>
{
    
    public string TokenHash { get; set; }
    public DateTime TokenExp { get; set; }
    public string RefreshTokenHash { get; set; }
    public DateTime RefreshTokenExp { get; set; }

    public string Device { get; set; }

    public UserEntity User { get; set; }
    public Guid UserId { get; set; }
}
