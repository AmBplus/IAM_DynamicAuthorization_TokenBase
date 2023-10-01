using Base.Shared.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Entities;

public abstract class BaseEntity<TId>
{
    public virtual TId Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime LastModifiedDate { get; set; }  
    public BaseEntity ()
    {
        {
            CreatedDate = Base.Shared.Date.DateUtility.DateTimeNow;
            LastModifiedDate = DateUtility.DateTimeNow;
        }


    }
}