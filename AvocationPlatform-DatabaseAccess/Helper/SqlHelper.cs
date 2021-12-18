using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AvocationPlatform_DatabaseAccess.Helper
{

    public static class SqlHelper
    {
        public static IEnumerable<T> Query<T>(string query, dynamic param = null,
               dynamic outParam = null, SqlTransaction transaction = null,
               bool buffered = true, int? commandTimeout = null, string connectionString = null) where T : class
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            var output = connection.Query<T>(query, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.Text);
            connection.Close();
            return output;
        }

        public static IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null,
               dynamic outParam = null, SqlTransaction transaction = null,
               bool buffered = true, int? commandTimeout = null, string connectionString = null) where T : class
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            var output = connection.Query<T>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);
            connection.Close();
            return output;
        }

        public static int ExecuteSP(string storedProcedure, dynamic param = null,
            dynamic outParam = null, SqlTransaction transaction = null,
            int? commandTimeout = null, string connectionString = null)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            var output = connection.Execute(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);
            connection.Close();
            return output;
        }

        public static object ExecuteScalar(string storedProcedure, dynamic param = null,
            dynamic outParam = null, SqlTransaction transaction = null,
            int? commandTimeout = null, string connectionString = null)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            var output = connection.ExecuteScalar(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);
            connection.Close();
            return output;
        }

        public static object QueryMultiple(string storedProcedure, dynamic param = null,
            dynamic outParam = null, SqlTransaction transaction = null,
            int? commandTimeout = null, string connectionString = null)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            var output = connection.QueryMultiple(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);
            connection.Close();
            return output;
        }

        private static void CombineParameters(ref dynamic param, dynamic outParam = null)
        {
            if (outParam != null)
            {
                if (param != null)
                {
                    param = new DynamicParameters(param);
                    ((DynamicParameters)param).AddDynamicParams(outParam);
                }
                else
                {
                    param = outParam;
                }
            }
        }

        private static int ConnectionTimeout { get; set; }

        private static int GetTimeout(int? commandTimeout = null)
        {
            if (commandTimeout.HasValue)
                return commandTimeout.Value;

            return ConnectionTimeout;
        }
    }
}
