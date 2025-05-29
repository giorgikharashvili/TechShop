using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;


namespace TechShop.Infrastructure.Data
{
    public class DapperContext
    {

        private readonly string? _connectionString;

        public DapperContext()
        {
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
            if(string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string not found in environment variables.");
            }
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                Console.WriteLine("Database connection successful.");
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database connection failed: " + ex.Message);
                throw; 
            }
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
