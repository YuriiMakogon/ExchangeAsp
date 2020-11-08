using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ExchangeAsp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExchangeAsp.Pages.Exchange
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        private readonly Exchenger _ex;
        public IndexModel(ApplicationDBContext db)
        {
            _db = db;
           
        }

        public IEnumerable<Rates> Rates { get; set; }
        [BindProperty]
        public Rates rates { get; set; }
        public async Task OnGet()
        {
             Rates = await _db.Rates.ToListAsync();
        }

        
        public IActionResult index1(Rates rates)
        {
            string url = string.Format(urlPattern, rates.ToAmount, rates.ToCurrency);
            WebClient wc = new WebClient();
            var json = wc.DownloadString(url + rates.ToAmount);


            Exchange exchange = Newtonsoft.Json.JsonConvert.DeserializeObject<Exchange>(json);
            double exchangeRate = exchange.rates[rates.ToCurrency];

            rates.converts = rates.Amount * exchangeRate;
            //ViewBag.Convert = rates.converts;
            return Page();
        }




















        private const string urlPattern = "https://api.exchangeratesapi.io/latest?base=";
        public string CurrencyConversion(float amount, string fromCurrency, string toCurrency)
        {
            
            

            string url = string.Format(urlPattern, Rates.FirstOrDefault().ToAmount.ToString(), Rates.FirstOrDefault().ToCurrency.ToString());

            using (var wc = new WebClient())
            {
                var json = wc.DownloadString(url + Rates.FirstOrDefault().ToAmount.ToString());

                Exchange exchange = Newtonsoft.Json.JsonConvert.DeserializeObject<Exchange>(json);
                double exchangeRate = exchange.rates[Rates.FirstOrDefault().ToCurrency.ToString()];

                return (amount * exchangeRate).ToString();
            }
        }
     
        public void Exchanger1()
        {

            var inpString = HttpContext.Request.Form["pleaseWork"];


        }

        class Exchange
        {
            public Dictionary<String, double> rates { get; set; }
        }
        











        //public void OnGet()
        //{
        //}
    }
}
