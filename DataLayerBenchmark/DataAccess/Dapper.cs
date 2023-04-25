using BenchmarkDotNet.Attributes;
using Dapper;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataLayerBenchmark.DataAccess
{
    public class Dapper
    {
        public static async Task<Customer> AddCustomerDapperAsyncMicrosoft()
        {
            Customer customer = new Customer() { Id = Guid.NewGuid(), Name = "AddedWithDapperMicrosoft", BirthYear = 1111 };
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";

            using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                //await connection.OpenAsync(); connectie wordt al intern geopend met de extensiemethode van dapper zoals ExecuteAsync en QueryFirstOrDefaultAsync

                var query = "INSERT INTO Customers (Id, Name, BirthYear) VALUES (@Id, @Name, @BirthYear)";
                await connection.ExecuteAsync(query, new { Id = customer.Id, Name = customer.Name, BirthYear = customer.BirthYear });

                return customer;
            }
        }
       
        //public static async Task<Customer> AddCustomerDapperAsyncSQL()
        //{
        //    Customer customer = new Customer() { Id = Guid.NewGuid(), Name = "AddedWithDapperSQL", BirthYear = 1111 };
        //    string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        //    using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        await connection.OpenAsync();

        //        var query = "INSERT INTO Customers (Id, Name, BirthYear) VALUES (@Id, @Name, @BirthYear)";
        //        await connection.ExecuteAsync(query, new { Id = customer.Id, Name = customer.Name, BirthYear = customer.BirthYear });

        //        return customer;
        //    }
        //}

        public static async Task<Customer> GetCustomerByIdDapperAsyncMicrosoft()
        {
            string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";

            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                //await connection.OpenAsync();

                Customer customer = await connection.QueryFirstOrDefaultAsync<Customer>(
                    "SELECT * FROM Customers WHERE Id = @Id",
                    new { Id = new Guid("7864746B-8077-48C4-022B-08DB4008485D") }
                );

                return customer;
            }
        }
        //public static async Task<Customer> GetCustomerByIdDapperAsyncSQL()
        //{
        //    string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";

        //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        await connection.OpenAsync();

        //        Customer customer = await connection.QueryFirstOrDefaultAsync<Customer>(
        //            "SELECT * FROM Customers WHERE Id = @Id",
        //            new { Id = new Guid("7864746B-8077-48C4-022B-08DB4008485D") }
        //        );

        //        return customer;
        //    }
        //}
        public static async Task<List<Customer>> GetCustomersByNameDapperAsyncMicrosoft()
        {
            string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                //await connection.OpenAsync();

                var result = await connection.QueryAsync<Customer>("SELECT * FROM Customers Where Name = @Name", new { Name = "AddedWithDapperMicrosoft" });
                return result.ToList();
            }
        }
        //public static async Task<List<Customer>> GetCustomersDapperAsyncSQL()
        //{
        //    string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
        //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        await connection.OpenAsync();
        //        var result = await connection.QueryAsync<Customer>("SELECT * FROM Customers");
        //        return result.ToList();
        //    }
        //}

        public static async Task<int> UpdateCustomerDapperAsyncMicrosoft()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                //await connection.OpenAsync();
                int rowsAffected = await connection.ExecuteAsync("UPDATE Customers SET Name = @newName WHERE Name = @Name", new { newName = "UpdatedWithDapperMicrosoft", Name = "AddedWithDapperMicrosoft"});
                if (rowsAffected > 0)
                {
                    return rowsAffected;
                }
                return 0;
            }
        }
        //public static async Task<Customer> UpdateCustomerDapperAsyncSQL()
        //{
        //    Customer customer = new Customer() { Id = new Guid("19FE92ED-5954-4301-AD61-08DB4019396B"), Name = "UpdatedWithDapperSQL", BirthYear = 1000 };
        //    string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        await connection.OpenAsync();
        //        string query = "UPDATE Customers SET Name = @Name, BirthYear = @BirthYear WHERE Id = @Id";
        //        int rowsAffected = await connection.ExecuteAsync(query, customer);
        //        if (rowsAffected > 0)
        //        {
        //            return customer;
        //        }
        //        return null;
        //    }
        //}

        public static async Task DeleteCustomersByNameDapperAsyncMicrosoft()
        {
            string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                //await connection.OpenAsync();
                await connection.ExecuteAsync("DELETE FROM Customers WHERE Name = @Name", new { Name = "UpdatedWithDapperMicrosoft" });
            }
        }

        //public static async Task DeleteCustomersDapperAsyncSQL()
        //{
        //    string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
        //    int birthYear = 1;
        //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        await connection.OpenAsync();
        //        await connection.ExecuteAsync("DELETE FROM Customers WHERE BirthYear = @birthYear", new { birthYear });
        //    }
        //}
    }
}
