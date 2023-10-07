using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Entities
{
    public class UserTokenEntity : BaseEntity<Guid>
    {
        public string TokenHash { get; set; }
        public DateTime TokenExp { get; set; }
        public string RefreshTokenHash { get; set; }
        public DateTime RefreshTokenExp { get; set; }

        public string Device { get; set; }

        public UserEntity User { get; set; }
        public Guid UserId { get; set; }
    }
}
