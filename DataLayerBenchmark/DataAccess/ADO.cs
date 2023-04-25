using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerBenchmark.DataAccess
{
    public class ADO
    {
        public static async Task<Customer> AddCustomerADOAsyncMicrosoft()
        {
            Customer customer = new Customer() { Id = Guid.NewGuid(), Name = "AddedWithAdoMicrosoft", BirthYear = 1111 };
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand("INSERT INTO Customers (Id, Name, BirthYear) VALUES (@Id, @Name, @BirthYear)", connection);
                command.Parameters.AddWithValue("@Id", customer.Id);
                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@BirthYear", customer.BirthYear);

                await connection.OpenAsync();

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return customer;
                }

                return null;
            }
        }
        //public static async Task<Customer> AddCustomerADOAsyncSQL()
        //{
        //    Customer customer = new Customer() { Id = Guid.NewGuid(), Name = "AddedWithAdoSQL", BirthYear = 1111 };
        //    string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("INSERT INTO Customers (Id, Name, BirthYear) VALUES (@Id, @Name, @BirthYear)", connection);
        //        command.Parameters.AddWithValue("@Id", customer.Id);
        //        command.Parameters.AddWithValue("@Name", customer.Name);
        //        command.Parameters.AddWithValue("@BirthYear", customer.BirthYear);

        //        await connection.OpenAsync();

        //        int rowsAffected = await command.ExecuteNonQueryAsync();

        //        if (rowsAffected > 0)
        //        {
        //            return customer;
        //        }

        //        return null;
        //    }
        //}

        public static async Task<Customer> GetCustomerByIdADOAsyncMicrosoft()
        {
            string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand("SELECT * FROM Customers WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", new Guid("7864746B-8077-48C4-022B-08DB4008485D"));
                //Customer? customer = null;

                await connection.OpenAsync();

                using (Microsoft.Data.SqlClient.SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Customer
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            BirthYear = reader.GetInt32(2)
                        };
                    }
                }

                return null;
            }
        }
        //public static async Task<Customer> GetCustomerByIdADOAsyncSQL()
        //{
        //    string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
        //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("SELECT * FROM Customers WHERE Id = @Id", connection);
        //        command.Parameters.AddWithValue("@Id", new Guid("7864746B-8077-48C4-022B-08DB4008485D"));
        //        //Customer? customer = null;

        //        await connection.OpenAsync();

        //        using (System.Data.SqlClient.SqlDataReader reader = await command.ExecuteReaderAsync())
        //        {
        //            if (await reader.ReadAsync())
        //            {
        //                return new Customer
        //                {
        //                    Id = reader.GetGuid(0),
        //                    Name = reader.GetString(1),
        //                    BirthYear = reader.GetInt32(2)
        //                };
        //            }
        //        }

        //        return null;
        //    }
        //}

        public static async Task<List<Customer>> GetCustomersByNameADOAsyncMicrosoft()
        {
            string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand("SELECT * FROM Customers WHERE Name = @Name", connection);
                command.Parameters.AddWithValue("@Name", "AddedWithAdoMicrosoft");
                List<Customer> customers = new List<Customer>();

                await connection.OpenAsync();

                using (Microsoft.Data.SqlClient.SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        customers.Add(new Customer
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            BirthYear = reader.GetInt32(2)
                        });
                    }
                }

                return customers;
            }
        }
        //public static async Task<List<Customer>> GetCustomersADOAsyncSQL()
        //{
        //    string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
        //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("SELECT * FROM Customers", connection);
        //        List<Customer> customers = new List<Customer>();

        //        await connection.OpenAsync();

        //        using (System.Data.SqlClient.SqlDataReader reader = await command.ExecuteReaderAsync())
        //        {
        //            while (await reader.ReadAsync())
        //            {
        //                customers.Add(new Customer
        //                {
        //                    Id = reader.GetGuid(0),
        //                    Name = reader.GetString(1),
        //                    BirthYear = reader.GetInt32(2)
        //                });
        //            }
        //        }

        //        return customers;
        //    }
        //}

        public static async Task<int> UpdateCustomerADOAsyncMicrosoft()
        {
            Customer customer = new Customer() { Id = new Guid("19FE92ED-5954-4301-AD61-08DB4019396B"), Name = "AddedWithAdoMicrosoft", BirthYear = 1000 };
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand("UPDATE Customers SET Name = @newName WHERE Name = @Name", connection);
                command.Parameters.AddWithValue("@newName", "UpdatedWithAdoMicrosoft");
                command.Parameters.AddWithValue("@Name", customer.Name);
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    return rowsAffected;
                }
            }
            return 0;
        }
        //public static async Task<Customer> UpdateCustomerADOAsyncSQL()
        //{
        //    Customer customer = new Customer() { Id = new Guid("19FE92ED-5954-4301-AD61-08DB4019396B"), Name = "UpdatedWithAdoSql", BirthYear = 1000 };
        //    string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        await connection.OpenAsync();
        //        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("UPDATE Customers SET Name = @Name WHERE Id = @Id", connection);
        //        command.Parameters.AddWithValue("@Name", customer.Name);
        //        command.Parameters.AddWithValue("@Id", customer.Id);
        //        int rowsAffected = await command.ExecuteNonQueryAsync();
        //        if (rowsAffected > 0)
        //        {
        //            return customer;
        //        }
        //    }
        //    return null;
        //}

        public static async Task DeleteCustomersByNameADOAsyncMicrosoft()
        {
            string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
            
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand("DELETE FROM Customers WHERE Name = @Name", connection);
                command.Parameters.AddWithValue("@Name", "UpdatedWithAdoMicrosoft");

                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }
        //public static async Task DeleteCustomersWithBirthYearADOAsyncSQL()
        //{
        //    string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
        //    int birthYear = 1;

        //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
        //    {
        //        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("DELETE FROM Customers WHERE BirthYear = @birthYear", connection);
        //        command.Parameters.AddWithValue("@birthYear", birthYear);

        //        await connection.OpenAsync();

        //        await command.ExecuteNonQueryAsync();
        //    }
        //}

    }
}
