using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerBenchmark
{
    internal class CustomerDbContext : DbContext
    {
        

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true");
                          //.LogTo(Console.WriteLine);
        }

        

        public static void SeedDb()
        {
            using (var context = new CustomerDbContext())
            {
                if (!context.Customers.Any())
                {
                    for (int i = 0; i < 100; i++)
                    {
                        var customer = new Customer { Name = "John Smith", BirthYear = 2000 };
                        context.Customers.Add(customer);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
