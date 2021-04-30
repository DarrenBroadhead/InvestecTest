using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repository.Extensions
{
    public static class SqlExtension
    {
        private const string ConnectionString = @"Data Source=(local);Database=CustomerOrders;User Id=admin;Password=password;";

        public static SqlConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);

            return connection;
        }

        public static SqlCommand GetCommand(this string sqlString, SqlConnection connection)
        {
            var command = new SqlCommand(sqlString, connection);

            return command;
        }

        public static SqlDataReader GetReader(this string sqlString, SqlConnection connection)
        {
            var command = new SqlCommand(sqlString, connection);
            SqlDataReader reader = command.ExecuteReader();

            return reader;
        }
    }
}