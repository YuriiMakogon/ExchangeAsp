using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeAsp.Model
{
    public class Change : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index1(Rates model)
        {
            string result = model.Amount.ToString();
            string numberA = model.converts.ToString();


            ViewBag.NumberA = numberA;
            ViewBag.Resut = result;

            string ToAmount = model.ToAmount.ToString();
            string ToCurrency = model.ToCurrency.ToString();

            model.ToAmount = model.ToCurrency;
            model.ToCurrency = model.ToAmount;


            //model.Result = ViewBag.NumberA;
            return View();
        }


    }
}

