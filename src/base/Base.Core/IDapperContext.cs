using System.Data;

namespace Base.Core
{
    public interface IDapperContext : IDisposable
    {
         IDbConnection CreateConnection();
    }
}