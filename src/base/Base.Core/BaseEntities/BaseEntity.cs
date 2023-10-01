

using Base.Shared.Date;
using Base.Shared.ResultUtility;

namespace Base.Core.BaseEntities
{

    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdateDate { get; set; }
        public bool IsRemoved { get;private  set; } = false;
        public BaseEntity()
        {
            CreatedDate = DateUtility.DateTimeNow;
        }

        public virtual ResultOperation Remove()
        {
            IsRemoved = true;
            return ResultOperation.ToSuccessResult();
        }
        public virtual ResultOperation Restore()
        {
            IsRemoved = false;
            return ResultOperation.ToSuccessResult();
        }
    }
}
