using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AccessManagement.Entities;


public class ApplicationUser : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireTime { get; set; }
    public bool IsActive { get; set; }
}
