using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace SqlConnections.SqlConnections
{
    public static class ConnectionSql
    {
        private static IConfiguration _configuration;
        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private static string GetConnectionString()
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException("A configuração não foi inicializada.");
            }

            return _configuration.GetConnectionString("FINANCAS");
        }
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
    }
}
