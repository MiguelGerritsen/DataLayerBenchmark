using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace DataLayerBenchmark
{
    public class Customer
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual int BirthYear { get; set; }

        
    }
}
