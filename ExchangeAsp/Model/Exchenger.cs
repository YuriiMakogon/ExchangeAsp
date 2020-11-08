using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExchangeAsp.Model
{
    public class Exchenger : Controller
    {


        private const string urlPattern = "https://api.exchangeratesapi.io/latest?base=";

        public IActionResult Index(Rates model)
        {

            string url = string.Format(urlPattern, model.ToAmount.ToString(), model.ToCurrency.ToString());

            WebClient wc = new WebClient();
            var json = wc.DownloadString(url + model.ToAmount.ToString());


            Exchange exchange = Newtonsoft.Json.JsonConvert.DeserializeObject<Exchange>(json);
            double exchangeRate = exchange.rates[model.ToCurrency.ToString()];

            model.converts = model.Amount * exchangeRate;
            ViewBag.Convert = model.converts;


            string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Database=History;Trusted_Connection=True;MultipleActiveResultSets=true";
            string sql = "insert into History (FromCurrency,FromAmount,ToCurrency,ToAmount,Date1) values ('" + model.Amount + "','" + model.ToAmount + "','" + model.ToCurrency + "','" + model.converts + "','" + DateTime.Now + "');";


            SqlConnection sqlConnection = new SqlConnection(connetionString);
            sqlConnection.Open();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            sqlReader = command.ExecuteReader();
            sqlReader.Read();
            sqlReader.Close();


            return View();
        }


        class Exchange
        {
            public Dictionary<String, double> rates { get; set; }
        }








        //    public IActionResult CalPage()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    public IActionResult Add()
        //    {
        //        try
        //        {
        //            int num1 = Convert.ToInt32(HttpContext.Request.Form["Text1"].ToString());
        //            int num2 = Convert.ToInt32(HttpContext.Request.Form["Text2"].ToString());
        //            ViewBag.Result = (num1 + num2).ToString();
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Result = "Wrong Input Provided.";
        //        }
        //        return View("CalPage");
        //    }

        //    [HttpPost]
        //    public IActionResult Minus()
        //    {
        //        try
        //        {
        //            int num1 = Convert.ToInt32(HttpContext.Request.Form["Text1"].ToString());
        //            int num2 = Convert.ToInt32(HttpContext.Request.Form["Text2"].ToString());
        //            ViewBag.Result = (num1 - num2).ToString();
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Result = "Wrong Input Provided.";
        //        }
        //        return View("CalPage");
        //    }

        //    [HttpPost]
        //    public IActionResult Multiply()
        //    {
        //        try
        //        {
        //            int num1 = Convert.ToInt32(HttpContext.Request.Form["Text1"].ToString());
        //            int num2 = Convert.ToInt32(HttpContext.Request.Form["Text2"].ToString());
        //            ViewBag.Result = (num1 * num2).ToString();
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Result = "Wrong Input Provided.";
        //        }
        //        return View("CalPage");
        //    }

        //    [HttpPost]
        //    public IActionResult Divide()
        //    {
        //        try
        //        {
        //            decimal num1 = Convert.ToDecimal(HttpContext.Request.Form["Text1"].ToString());
        //            decimal num2 = Convert.ToDecimal(HttpContext.Request.Form["Text2"].ToString());
        //            decimal f = num1 / num2;
        //            ViewBag.Result = f.ToString();
        //        }
        //        catch (Exception)
        //        {
        //            ViewBag.Result = "Wrong Input Provided.";
        //        }
        //        return View("CalPage");
        //    }
        //}
    }
}

