using Microsoft.EntityFrameworkCore;

namespace DataLayerBenchmark.DataAccess
{
    public class EFCore
    {
        public static async Task<Customer> AddCustomerEFCoreAsync()
        {
            Customer customer = new Customer() { Id = Guid.NewGuid(), Name = "AddedWithEFCore", BirthYear = 1111 };
            using (CustomerDbContext context = new CustomerDbContext())
            {
                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync();
                return customer;
            }
        }
        public static async Task<Customer> GetCustomerByIdEfCoreAsync()
        {
            using (CustomerDbContext context = new CustomerDbContext())
            {
                return await context.Customers.FirstOrDefaultAsync(c => c.Id == Guid.Parse("7864746B-8077-48C4-022B-08DB4008485D"));
            }
        }
        public static async Task<List<Customer>> GetCustomersEFCoreAsync()
        {
            using (CustomerDbContext context = new CustomerDbContext())
            {
                return await context.Customers.Where(c => c.Name == "AddedWithEFCore").ToListAsync();
            }
        }
        public static async Task<int> UpdateCustomerEFCoreAsync()
        {
            using (var context = new CustomerDbContext())
            {
                var customersToUpdate = await context.Customers.Where(c => c.Name == "AddedWithEFCore").ToListAsync();
                foreach (var customer in customersToUpdate)
                {
                    customer.Name = "UpdatedWithEFCore";
                }
                int rowsAffected = await context.SaveChangesAsync();
                return rowsAffected;
            }
        }
        public static async Task DeleteCustomersEFCoreAsync()
        {
            using (CustomerDbContext context = new CustomerDbContext())
            {
                var customersToDelete = await context.Customers.Where(c => c.Name == "UpdatedWithEFCore").ToListAsync();
                context.Customers.RemoveRange(customersToDelete);
                await context.SaveChangesAsync();
            }
        }
    }
}
