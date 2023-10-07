using System.Data;

namespace Base.Core
{
    public interface IDapperContext : IAsyncDisposable,IDisposable
    {
         IDbConnection CreateConnection();
    }
}