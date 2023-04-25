using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using NHibernate.Linq;


namespace DataLayerBenchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class GetByIdBenchmark
    {
        [Benchmark(Baseline = true)]
        public async Task<Customer> GetCustomerByIdADOAsyncMicrosoft()
        {
            return await DataAccess.ADO.GetCustomerByIdADOAsyncMicrosoft();
        }

        //[Benchmark]
        //public async Task<Customer> GetCustomerByIdADOAsyncSQL()
        //{
        //    return await DataAccess.ADO.GetCustomerByIdADOAsyncSQL();
        //}

        [Benchmark]
        public async Task<Customer> GetCustomerByIdDapperAsyncMicrosoft()
        {
            return await DataAccess.Dapper.GetCustomerByIdDapperAsyncMicrosoft();
        }

        //[Benchmark]
        //public async Task<Customer> GetCustomerByIdDapperAsyncSQL()
        //{
        //    return await DataAccess.Dapper.GetCustomerByIdDapperAsyncSQL();
        //}

        [Benchmark]
        public async Task<Customer> GetCustomerByIdEfCoreAsync()
        {
            return await DataAccess.EFCore.GetCustomerByIdEfCoreAsync();
        }
        //ef raw query ^
        //SELECT TOP(1) c.Id, c.BirthYear, c.Name
        //FROM Customers AS c
        //WHERE c.Id == '7864746b-8077-48c4-022b-08db4008485d')

        [Benchmark]
        public async Task<Customer> GetCustomerByIdNHibernateAsync()
        {
            return await DataAccess.NHibernate.GetCustomerByIdNHibernateAsync();
        }
    }
}
