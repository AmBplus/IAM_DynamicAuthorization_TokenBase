using Base.Core;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Base.Infrastructure.Dapper
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;

        public DapperContext(DapperSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }
        private bool _disposed;

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
            _disposed = true;
        }
      
    }
}
