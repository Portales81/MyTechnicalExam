using FileUpload.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        //private string apiBaseUrl;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            List<TransactionsViewModel> list = new List<TransactionsViewModel>();
        //    using (HttpClient client = new HttpClient())
        //    {
        //        //StringContent content = new StringContent(JsonConvert.SerializeObject(list), Encoding.UTF8, "application/json");
        //        string endpoint = apiBaseUrl + "/transactions/all";
        //        // var responseTask = client.GetAsync(endpoint);
        //        //responseTask.Wait();

        //        HttpResponseMessage response = await client.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();
        //        string responseBody = await response.Content.ReadAsStringAsync();


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public  IActionResult GetAllTransactions()
        {
            IEnumerable<TransactionsViewModel> trans = new List<TransactionsViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:32935/");

                var response =  client.GetAsync("api/FileUpload/transactions/all").Result;

                string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                    
                    trans =  JsonConvert.DeserializeObject<IEnumerable<TransactionsViewModel>>(res);
                }
               
                   
                return View(trans.ToList());

            }
        }
    }
}
