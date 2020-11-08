using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeAsp.Model
{
    public class Rates
    {
        [Key]
        public int id { get; set; }
        [Required]
        public double Amount { get; set; }
        public string ToAmount { get; set; }

        public double converts { get; set; }
        public string ToCurrency { get; set; }
        public DateTime date { get; set; }

    }
}
