using BenchmarkDotNet.Attributes;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerBenchmark.Benchmarks
{
    [MemoryDiagnoser]
    public  class CreateBenchmark
    {
        [Benchmark(Baseline = true)]
        public async Task<Customer> AddCustomerADOAsyncMicrosoft()
        {
            return await DataAccess.ADO.AddCustomerADOAsyncMicrosoft();
        }

        //[Benchmark]
        //public async Task<Customer> AddCustomerADOAsyncSQL()
        //{
        //   return await DataAccess.ADO.AddCustomerADOAsyncSQL();
        //}

        [Benchmark]
        public async Task<Customer> AddCustomerDapperAsyncMicrosoft()
        {
            return await DataAccess.Dapper.AddCustomerDapperAsyncMicrosoft();
        }

        //[Benchmark]
        //public async Task<Customer> AddCustomerDapperAsyncSQL()
        //{
        //    return await DataAccess.Dapper.AddCustomerDapperAsyncSQL();
        //}
        [Benchmark]
        public async Task<Customer> AddCustomerEFCoreAsync()
        {
            return await DataAccess.EFCore.AddCustomerEFCoreAsync();
        }
        [Benchmark]
        public async Task<Customer> AddCustomerNHibernateAsync()
        {
            return await DataAccess.NHibernate.AddCustomerNHibernateAsync();
        }
    }
}
