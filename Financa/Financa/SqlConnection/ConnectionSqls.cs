//using System;
//using System.Data.SqlClient;
//using Microsoft.Extensions.Configuration;

//namespace Financa.ConnectionSqls
//{
//    public static class ConnectionSql
//    {
//        private static IConfiguration _configuration;
//        public static void Configure(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }
//        private static string GetConnectionString()
//        {
//            if (_configuration == null)
//            {
//                throw new InvalidOperationException("A configuração não foi inicializada.");
//            }

//            return _configuration.GetConnectionString("FINANCAS");
//        }
//        public static SqlConnection GetConnection()
//        {
//            return new SqlConnection(GetConnectionString());
//        }
//    }
//}
