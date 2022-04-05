using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelMetricsTest.Model
{
    public class Park
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EntryTime { get; set; }
        public string ExitTime { get; set; }
        public string HoursSpent { get; set; }
        public decimal AmountToPay { get; set; }
        public DateTime Date { get; set; }
    }
}
