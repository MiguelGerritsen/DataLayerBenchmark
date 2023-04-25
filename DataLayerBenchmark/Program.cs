using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using DataLayerBenchmark.Benchmarks;
using DataLayerBenchmark.DataAccess;
using System.Text;

namespace DataLayerBenchmark
{
    //JE MOET DE APPLICATIE IN RELEASE MODE RUNNING NIET IN DEBUG MODE OM DE BENCHMARKS TE LATEN WERKEN
    //dit is zodat het alle debug helpers niet aan staan en dan maakt de applicatie meer gelijk met productie
    //waardoor de benchmarks zo betrouwbaar mogelijk zijn
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //runs every method that is decorated with the benchmark attribute in the demo class
            //var results = BenchmarkRunner.Run<demo>(); 

            var results = BenchmarkRunner.Run<UpdateBenchmark>();

            //await DataAccess.Dapper.AddCustomerDapperAsyncMicrosoftNo();

            //var a = await DataAccess.NHibernate.GetCustomersNHibernateAsync();
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine($"{a[i].Id} {a[i].Name } {a[i].BirthYear }");
            //}

            //using (var context = new CustomerDbContext())
            //{
            //    context.Customers.Add(new Customer { Name = "miguel", BirthYear = 1000 });
            //    context.SaveChanges();

            //}

            //using (var context = new CustomerDbContext())
            //{
            //    var c = context.Customers.FirstOrDefault(x => x.Name == "miguel");
            //    Console.WriteLine(c.Id + c.Name + c.BirthYear);
            //}


            //GetByIdBenchmark getByIdBenchmark = new GetByIdBenchmark();

            ////ado works!
            //Customer customer = await getByIdBenchmark.GetCustomerByIdADOAsyncSQL();
            //Console.WriteLine(customer.Id + customer.Name + customer.BirthYear);

            ////dapper works
            //Customer customer2 = await getByIdBenchmark.GetCustomerByIdDapperAsync();
            //Console.WriteLine(customer2.Id + customer2.Name + customer2.BirthYear);

            ////EFcore works
            //Customer customer3 = await getByIdBenchmark.GetCustomerByIdEfCoreAsync();
            //Console.WriteLine(customer3.Id + customer3.Name + customer3.BirthYear);

            // NHibernate 1
            //Customer customer = await getByIdBenchmark.GetCustomerByIdNHibernateAsync();
            //Console.WriteLine(customer.Id + customer.Name + customer.BirthYear);

            //NHibernate 2
            //Customer customer = await getByIdBenchmark.GetCustomerByIdNHibernateAsync2();
            //Console.WriteLine(customer.Id + customer.Name + customer.BirthYear);

            //GetAllBenchmark getAllBenchmark = new GetAllBenchmark();

            ////ADO
            //var customers = await getAllBenchmark.GetCustomersADOAsyncSQL();
            //Console.Out.WriteLine(customers.Count + " AdoSQL");

            ////dapper
            //var customers2 = await getAllBenchmark.GetCustomersDapperAsyncSQL();
            //Console.Out.WriteLine(customers2.ToList().Count + " dapeprsql");

            ////efcore
            //var customers3 = await getAllBenchmark.GEtCustomersEFCoreAsync();
            //Console.Out.WriteLine(customers3.Count + " efcore");

            ////nhibernate
            //var customers4 = await getAllBenchmark.GetCustomersNHibernateAsync();
            //Console.Out.WriteLine(customers4.Count + " nhibernate");

            //create test
            //await ADO.AddCustomerADOAsyncMicrosoft(); works
            //await ADO.AddCustomerADOAsyncSQL(); works

            //await DataAccess.Dapper.AddCustomerDapperAsyncMicrosoft(); works
            //await DataAccess.Dapper.AddCustomerDapperAsyncSQL(); works

            //await EFCore.AddCustomerEFCoreAsync(); works

            //await DataAccess.NHibernate.AddCustomerNHibernateAsync(); works

            //update test
            //await DataAccess.ADO.UpdateCustomerADOAsyncMicrosoft(); works
            //await DataAccess.ADO.UpdateCustomerADOAsyncSQL(); works

            //await DataAccess.Dapper.UpdateCustomerDapperAsyncMicrosoft(); works
            //await DataAccess.Dapper.UpdateCustomerDapperAsyncSQL(); works

            //await DataAccess.EFCore.UpdateCustomerEFCoreAsync(); works
            //await DataAccess.NHibernate.UpdateCustomerNHibernateAsync(); works
        }


    }


    //[SimpleJob(RuntimeMoniker.Net70)] //met deze comment kan je verschillende versies van dotnet testen
    //[SimpleJob(RuntimeMoniker.Net60)] //je moet ze wel geinstalleerd hebben op je pc. in csproj file ook alle versies toevoegen
    [MemoryDiagnoser]//adds memory usage to the stats
    public class Demo
    {
        //[benchmark] voor alle methods die je wilt benchmarken
        //Baseline = true voor de benchmark die je als standaard wilt hebben voor de vergelijking met de anderen
        [Benchmark(Baseline = true)]
        public string GetFullStringNormally()
        {
            string output = "";

            for (int i = 0; i < 20; i++)
            {
                output += i;
            }
            return output;
        }

        [Benchmark]
        public string GetFullStringWithStringBuilder()
        { 
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < 20; i++)
            {
                output.Append(i);
            }
            return output.ToString();
        }
    }
}