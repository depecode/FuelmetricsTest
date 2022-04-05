using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelMetricsTest.Model
{
    public class Rule
    {
        public int Id { get; set; }
        public string RuleDescription { get; set; }
        public decimal Amount { get; set; }
    }
}
