using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerBenchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class UpdateBenchmark
    {
        [Benchmark(Baseline = true)]
        public async Task<int> UpdateCustomerADOAsyncMicrosoft()
        {
            return await DataAccess.ADO.UpdateCustomerADOAsyncMicrosoft();
        }
        //[Benchmark]
        //public async Task<Customer> UpdateCustomerADOAsyncSQL()
        //{
        //    return await DataAccess.ADO.UpdateCustomerADOAsyncSQL();
        //}
        [Benchmark]
        public async Task<int> UpdateCustomerDapperAsyncMicrosoft()
        {
            return await DataAccess.Dapper.UpdateCustomerDapperAsyncMicrosoft();
        }
        //[Benchmark]
        //public async Task<Customer> UpdateCustomerDapperAsyncSQL()
        //{
        //    return await DataAccess.Dapper.UpdateCustomerDapperAsyncSQL();
        //}
        [Benchmark]
        public async Task<int> UpdateCustomerEFCoreAsync()
        {
            return await DataAccess.EFCore.UpdateCustomerEFCoreAsync();
        }
        [Benchmark]
        public async Task<int> UpdateCustomerNHibernateAsync()
        {
            return await DataAccess.NHibernate.UpdateCustomersNHibernateAsync();
        }
    }
}
