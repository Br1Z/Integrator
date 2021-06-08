using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratorEHZ.Models
{
    public class SearchTheRange
    {
        public DateTime dateFrom { get; set; }
        public DateTime timeFrom { get; set; }
        public DateTime dateTo { get; set; }
        public DateTime timeTo { get; set; }
    }
}
