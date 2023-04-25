using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerBenchmark.DataAccess
{
    public class NHibernate
    {
        public static async Task<Customer> AddCustomerNHibernateAsync()
        {
            Customer customer = new Customer() { Id = Guid.NewGuid(), Name = "AddedWithNHibernate", BirthYear = 1111 };

            using (ISession session = NHibernateSession.GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    await session.SaveAsync(customer);
                    await transaction.CommitAsync();
                    return customer;
                }
            }
        }
        public static async Task<Customer> GetCustomerByIdNHibernateAsync()
        {
            using (ISession session = NHibernateSession.GetSession())
            {
                return await session.Query<Customer>().FirstOrDefaultAsync(c => c.Id == Guid.Parse("7864746B-8077-48C4-022B-08DB4008485D"));
            }
        }
        public static async Task<IList<Customer>> GetCustomersNHibernateAsync()
        {
            using (ISession session = NHibernateSession.GetSession())
            {
                var customers = await session.QueryOver<Customer>()
                    .Where(c => c.Name == "AddedWithNHibernate")
                    .ListAsync();
                return customers;
            }
        }
        public static async Task<int> UpdateCustomersNHibernateAsync()
        {
            using (var session = NHibernateSession.GetSession())
            using (var transaction = session.BeginTransaction())
            {
                var query = session.QueryOver<Customer>().Where(c => c.Name == "AddedWithNHibernate");
                var customers = await query.ListAsync();

                foreach (var customer in customers)
                {
                    customer.Name = "UpdatedWithNHibernate";
                    await session.UpdateAsync(customer);
                }

                await transaction.CommitAsync();
                return customers.Count;
            }
        }
        public static async Task DeleteCustomersNHibernateAsync()
        {
            using (var session = NHibernateSession.GetSession())
            using (var transaction = session.BeginTransaction())
            {
                var customersToDelete = await session.QueryOver<Customer>().Where(c => c.Name == "UpdatedWithNHibernate").ListAsync();
                foreach (var customer in customersToDelete)
                {
                    await session.DeleteAsync(customer);
                }
                await transaction.CommitAsync();
            }
        }
    }
}
