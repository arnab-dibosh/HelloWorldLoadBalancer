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
        string[] ipList = new string[] {
            "192.168.100.12", "192.168.100.13", "192.168.100.14",
            "192.168.100.22", "192.168.100.23", "192.168.100.24",
            "192.168.100.34", "192.168.100.35", "192.168.100.36" };

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
            try {

                for (int i = 0; i < 3; i++) {

                    Random r = new Random();
                    int randomNum = r.Next(0, 8);
                    string serverIp = ipList[randomNum];

                    if (IsApiActive(serverIp)) {

                        var APIURL = $"http://{serverIp}:5051";
                        var machineName = Environment.MachineName;
                        ViewBag.MachineName = machineName;

                        var response = await client.PostAsJsonAsync(APIURL + "SampleInsert", machineName);
                        string responseBody = await response.Content.ReadAsStringAsync();
                        string[] authorsList = responseBody.Split(",");
                        ViewBag.ReturnMessage = "Insert Successful via " + authorsList[1] + " RecordId: " + authorsList[0];

                        break;
                    }

                }
            }
            catch (Exception ex) {

                ViewBag.ReturnMessage = ex.Message;
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
