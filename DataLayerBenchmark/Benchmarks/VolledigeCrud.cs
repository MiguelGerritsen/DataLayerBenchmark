using BenchmarkDotNet.Attributes;
using Dapper;
using NHibernate;

namespace DataLayerBenchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class VolledigeCrud
    {
        [Benchmark(Baseline = true)]
        public async Task AdoNetFullCrudBenchmarkAsyncMicrosoft()
        {
            // Create a new customer
            Customer newCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                BirthYear = 1980
            };
            await AddCustomerADOAsyncMicrosoft(newCustomer);

            // Get the customer by ID
            Customer customerById = await GetCustomerByIdADOAsyncMicrosoft(newCustomer.Id);

            // Update the customer
            customerById.Name = "Jane Doe";
            await UpdateCustomerADOAsyncMicrosoft(customerById);

            // Delete the customer
            await DeleteCustomerADOAsyncMicrosoft(customerById.Id);
        }
        [Benchmark]
        public async Task DapperFullCrudBenchmarkAsyncMicrosoft()
        {
            // Create a new customer
            Customer newCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                BirthYear = 1980
            };
            await AddCustomerDapperAsyncMicrosoft(newCustomer);

            // Get the customer by ID
            Customer customerById = await GetCustomerByIdDapperAsyncMicrosoft(newCustomer.Id);

            // Update the customer
            customerById.Name = "Jane Doe";
            await UpdateCustomerDapperAsyncMicrosoft(customerById);

            // Delete the customer
            await DeleteCustomerDapperAsyncMicrosoft(customerById.Id);
        }
        [Benchmark]
        public async Task EFCoreFullCrudBenchmarkAsync()
        {
            // Create a new customer
            Customer newCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                BirthYear = 1980
            };
            await AddCustomerEFCoreAsync(newCustomer);

            // Get the customer by ID
            Customer customerById = await GetCustomerByIdEFCoreAsync(newCustomer.Id);

            // Update the customer
            customerById.Name = "Jane Doe";
            await UpdateCustomerEFCoreAsync(customerById);

            // Delete the customer
            await DeleteCustomerEFCoreAsync(customerById.Id);
        }
        

        [Benchmark]
        public async Task NHibernateFullCrudBenchmarkAsync()
        {
            // Create a new customer
            Customer newCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                BirthYear = 1980
            };
            await AddCustomerNHibernateAsync(newCustomer);

            // Get the customer by ID
            Customer customerById = await GetCustomerByIdNHibernateAsync(newCustomer.Id);

            // Update the customer
            customerById.Name = "Jane Doe";
            await UpdateCustomerNHibernateAsync(customerById);

            // Delete the customer
            await DeleteCustomerNHibernateAsync(customerById.Id);
        }

        private async Task<Customer> AddCustomerADOAsyncMicrosoft(Customer customer)
        {
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
        private async Task<int> DeleteCustomerADOAsyncMicrosoft(Guid id)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (Microsoft.Data.SqlClient.SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Customers WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }
        private async Task<Customer> UpdateCustomerADOAsyncMicrosoft(Customer customer)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("UPDATE Customers SET Name = @Name, BirthYear = @BirthYear WHERE Id = @Id", connection);
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
        private async Task<Customer> GetCustomerByIdADOAsyncMicrosoft(Guid id)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("SELECT * FROM Customers WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();

                using (System.Data.SqlClient.SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                        customer.Name = reader.GetString(reader.GetOrdinal("Name"));
                        customer.BirthYear = reader.GetInt32(reader.GetOrdinal("BirthYear"));
                        return customer;
                    }

                    return null;
                }
            }
        }

        private async Task<Customer> AddCustomerDapperAsyncMicrosoft(Customer customer)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                string query = "INSERT INTO Customers (Id, Name, BirthYear) VALUES (@Id, @Name, @BirthYear)";
                int rowsAffected = await connection.ExecuteAsync(query, customer);

                if (rowsAffected > 0)
                {
                    return customer;
                }

                return null;

            }
        }
        private async Task<int> DeleteCustomerDapperAsyncMicrosoft(Guid id)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                string query = "DELETE FROM Customers WHERE Id = @Id";

                int rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

                return rowsAffected;
            }
        }
        private async Task<Customer> UpdateCustomerDapperAsyncMicrosoft(Customer customer)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                string query = "UPDATE Customers SET Name = @Name, BirthYear = @BirthYear WHERE Id = @Id";

                int rowsAffected = await connection.ExecuteAsync(query, customer);

                if (rowsAffected > 0)
                {
                    return customer;
                }

                return null;
            }
        }
        private async Task<Customer> GetCustomerByIdDapperAsyncMicrosoft(Guid id)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CustomerBenchmarkDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Customers WHERE Id = @Id";

                Customer customer = await connection.QueryFirstOrDefaultAsync<Customer>(query, new { Id = id });

                return customer;
            }
        }

        private async Task<Customer> AddCustomerEFCoreAsync(Customer customer)
        {
            using (var context = new CustomerDbContext())
            {
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
            }

            return customer;
        }
        private async Task<int> DeleteCustomerEFCoreAsync(Guid id)
        {
            using (var context = new CustomerDbContext())
            {
                var customer = await context.Customers.FindAsync(id);
                context.Customers.Remove(customer);
                return await context.SaveChangesAsync();
            }
        }
        private async Task<Customer> UpdateCustomerEFCoreAsync(Customer customer)
        {
            using (var context = new CustomerDbContext())
            {
                context.Customers.Update(customer);
                await context.SaveChangesAsync();
            }

            return customer;
        }
        private async Task<Customer> GetCustomerByIdEFCoreAsync(Guid id)
        {
            using (var context = new CustomerDbContext())
            {
                return await context.Customers.FindAsync(id);
            }
        }

        public async Task<Customer> AddCustomerNHibernateAsync(Customer customer)
        {
            using (ISession session = NHibernateSession.GetSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                await session.SaveAsync(customer);
                await transaction.CommitAsync();
                return customer;
            }
        }
        public async Task<Customer> GetCustomerByIdNHibernateAsync(Guid id)
        {
            using (ISession session = NHibernateSession.GetSession())
            {
                return await session.GetAsync<Customer>(id);
            }
        }
        public async Task<Customer> UpdateCustomerNHibernateAsync(Customer customer)
        {
            using (ISession session = NHibernateSession.GetSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(customer);
                await transaction.CommitAsync();
                return customer;
            }
        }
        public async Task<int> DeleteCustomerNHibernateAsync(Guid id)
        {
            using (ISession session = NHibernateSession.GetSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                var customer = await session.GetAsync<Customer>(id);
                if (customer == null)
                {
                    return 0;
                }
                session.Delete(customer);
                await transaction.CommitAsync();
                return 1;
            }
        }

        public async Task EfCoreFullCrudBenchmarkAsyncNoExternal()
        {
            using (var context = new CustomerDbContext())
            {
                // Create a new customer
                Customer newCustomer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    BirthYear = 1980
                };
                context.Customers.Add(newCustomer);
                await context.SaveChangesAsync();

                // Get the customer by ID
                Customer customerById = await context.Customers.FindAsync(newCustomer.Id);

                // Update the customer
                customerById.Name = "Jane Doe";
                await context.SaveChangesAsync();

                // Delete the customer
                context.Customers.Remove(customerById);
                await context.SaveChangesAsync();
            }
        }
    }
}
