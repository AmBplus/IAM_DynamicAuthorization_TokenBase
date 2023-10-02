using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Entities
{
    public class UserProfileEntity : BaseEntity<Guid>
    {
        public string FatherName { get; set; }
        public DateTime BornDate { get; set; }
        public UserEntity ApplicationUser { get; set; }    
        public Guid ApplicationUserId { get; set; }
    }
}
