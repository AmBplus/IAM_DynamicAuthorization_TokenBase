using Base.Infrastructure.Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Data
{
    public class DapperAccessManagementDbContext : IDapperAccessManagementDbContext
    {
        
        private readonly string _connectionString;
        public DapperAccessManagementDbContext(DapperSettings dapperSettings)
        {
            _connectionString = dapperSettings.ConnectionString;
        }
        public IDbConnection CreateConnection()
               => new SqlConnection(_connectionString);

        IDisposable? _disposableResource = new MemoryStream();
        IAsyncDisposable? _asyncDisposableResource = new MemoryStream();

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);

            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposableResource?.Dispose();
                _disposableResource = null;

                if (_asyncDisposableResource is IDisposable disposable)
                {
                    disposable.Dispose();
                    _asyncDisposableResource = null;
                }
            }
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_asyncDisposableResource is not null)
            {
                await _asyncDisposableResource.DisposeAsync().ConfigureAwait(false);
            }

            if (_disposableResource is IAsyncDisposable disposable)
            {
                await disposable.DisposeAsync().ConfigureAwait(false);
            }
            else
            {
                _disposableResource?.Dispose();
            }

            _asyncDisposableResource = null;
            _disposableResource = null;
        }
    }
}
