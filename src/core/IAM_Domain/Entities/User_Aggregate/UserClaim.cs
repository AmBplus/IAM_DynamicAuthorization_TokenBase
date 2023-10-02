using AccessManagement.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Entities;

public class UserClaimEntity : IdentityUserClaim<Guid>
{
}
