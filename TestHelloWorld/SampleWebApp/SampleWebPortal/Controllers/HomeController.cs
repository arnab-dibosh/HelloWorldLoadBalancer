using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SampleWebPortal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SampleWebPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<HomeController> _logger;
        private static HttpClient client = new HttpClient();
        List<string> ipList = new List<string>();

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration) {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index() {
            ViewBag.MachineName = Environment.MachineName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SampleInsert() {
            var APIURL = string.Empty;
            try {

                string urlString = _configuration["ApiServerList"];
                string[] urlListFromAppSettings = urlString.Split(",");
                int maxRange = 8;

                for (int i = 0; i < 9; i++)
                    ipList.Add(urlListFromAppSettings[i]);

                for (int i = 0; i < 3; i++) {

                    Random r = new Random();
                    int randomNum = r.Next(0, maxRange);
                    string serverIp = ipList[randomNum];

                    if (IsApiActive(serverIp)) {

                        APIURL = $"http://{serverIp}:5051/SampleInsert/";
                        var machineName = Environment.MachineName;
                        ViewBag.MachineName = machineName;

                        var response =  client.PostAsJsonAsync(APIURL, machineName);
                        string responseBody = await response.Result.Content.ReadAsStringAsync();
                        string[] returnList = responseBody.Split(",");
                        ViewBag.ReturnMessage = "Insert Successful via " + returnList[1] + " RecordId: " + returnList[0];

                        break;
                    }
                    else {
                        maxRange--;
                        ipList.Remove(serverIp);
                    }

                }
            }
            catch (Exception ex) {

                ViewBag.ReturnMessage =APIURL+ ex.Message;
            }

            return View("Index");
        }

        private bool IsApiActive(string url) {

            bool output = true;
            var ping = new System.Net.NetworkInformation.Ping();

            var result = ping.Send(url);

            if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
                output = false;

            return output;
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
