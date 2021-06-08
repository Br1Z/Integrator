using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratorEHZ.Models
{
    public class AboutDevices
    {
        public DataSKZ dataSKZ { get; set; }
        public IEnumerable<LogSKZ> logSKZs { get; set; }

    }
}