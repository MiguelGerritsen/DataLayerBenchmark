using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataLayerBenchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class DeleteBenchmark
    {
        //[Benchmark(Baseline = true)]
        //public async Task DeleteCustomersWithBirthYearADOAsyncSQL()
        //{
        //    await DataAccess.ADO.DeleteCustomersWithBirthYearADOAsyncSQL();
        //}

        [Benchmark(Baseline = true)]
        public async Task DeleteCustomersByNameADOAsyncMicrosoft()
        {
            await DataAccess.ADO.DeleteCustomersByNameADOAsyncMicrosoft();
        }

        //[Benchmark]
        //public async Task DeleteCustomersDapperAsyncSQL()
        //{
        //    await DataAccess.Dapper.DeleteCustomersDapperAsyncSQL();
        //}

        [Benchmark]
        public async Task DeleteCustomersDapperByNameAsyncMicrosoft()
        {
            await DataAccess.Dapper.DeleteCustomersByNameDapperAsyncMicrosoft();
        }

        [Benchmark]
        public async Task DeleteCustomersEFCoreAsync()
        {
            await DataAccess.EFCore.DeleteCustomersEFCoreAsync();
        }

        [Benchmark]
        public async Task DeleteCustomersNHibernateAsync()
        {
            await DataAccess.NHibernate.DeleteCustomersNHibernateAsync();
        }
    }
}
