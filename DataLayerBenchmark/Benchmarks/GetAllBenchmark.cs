using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerBenchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class GetAllBenchmark
    {
        [Benchmark(Baseline = true)]
        public async Task<List<Customer>> GetCustomersByNameADOAsyncMicrosoft()
        {
            return await DataAccess.ADO.GetCustomersByNameADOAsyncMicrosoft();
        }

        //[Benchmark]
        //public async Task<List<Customer>> GetCustomersADOAsyncSQL()
        //{
        //    return await DataAccess.ADO.GetCustomersADOAsyncSQL();
        //}

        [Benchmark]
        public async Task<List<Customer>> GetCustomersByNameDapperAsyncMicrosoft()
        {
            return await DataAccess.Dapper.GetCustomersByNameDapperAsyncMicrosoft();
        }

        //[Benchmark]
        //public async Task<List<Customer>> GetCustomersDapperAsyncSQL()
        //{
        //    return await DataAccess.Dapper.GetCustomersDapperAsyncSQL();
        //}

        [Benchmark]
        public async Task<List<Customer>> GetCustomersEFCoreAsync()
        {
            return await DataAccess.EFCore.GetCustomersEFCoreAsync();
        }

        [Benchmark]
        public async Task<IList<Customer>> GetCustomersNHibernateAsync()
        {
            return await DataAccess.NHibernate.GetCustomersNHibernateAsync();
        }
    }
}
