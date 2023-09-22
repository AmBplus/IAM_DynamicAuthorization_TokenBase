using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Base.Shared
{
    public interface IWritableOptions<out T> : IOptions<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
    }

}
