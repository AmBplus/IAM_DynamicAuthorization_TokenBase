using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AccessManagement.Entities;


public class UserEntity : IdentityUser<Guid>
{

    public bool IsActive { get; set; } = true;
    public bool IsBanned { get; set; } = false;
    
}
