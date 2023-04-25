using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace DataLayerBenchmark
{
    public class NHibernateSession
    {
        public static ISession GetSession()
        {
            //eerst zonder folder structuur en als niet lukt models an mappings toeveogen
            string connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = CustomerBenchmarkDb; Trusted_Connection = True; MultipleActiveResultSets = true";
            var config = new Configuration();
            config.DataBaseIntegration(d =>
            {
                d.ConnectionString = connectionString;
                d.Dialect<MsSql2012Dialect>();
                d.Driver<SqlClientDriver>();
            });

            config.AddAssembly(Assembly.GetExecutingAssembly());
            ISessionFactory sessionFactory = config.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
