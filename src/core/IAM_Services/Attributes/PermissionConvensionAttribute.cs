using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface )]
    public class PermissionConventionAttribute : Attribute
    {
        public PermissionConventionAttribute() { }

        public string Name { get; set; }    
        public string Action { get; set; }    
        public string GroupName { get; set; }   
        public string Module { get; set; }
        public bool IsEnabled { get; set; }
    }
    public class CQRSPermissionConventionAttribute  : PermissionConventionAttribute
    {

    }
    public class AnonymousByPermissionAttribute : Attribute { }
}
