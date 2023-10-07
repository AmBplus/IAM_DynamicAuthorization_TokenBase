using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Data
{
    public class DapperSettings
    {
        public DapperSettings(string connectionString) { ConnectionString = connectionString; } 
        public string ConnectionString { get; }
    }
}
