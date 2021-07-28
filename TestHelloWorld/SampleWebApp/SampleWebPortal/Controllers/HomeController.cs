﻿using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.MachineName = Environment.MachineName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SampleInsertAsync()
        {
            var APIURL = _configuration["SampleTestAPIURL"];
            var machineName = Environment.MachineName;
            ViewBag.MachineName = machineName;
            try
            {
                var response = await client.PostAsJsonAsync(APIURL + "SampleInsert", machineName);
                string responseBody = await response.Content.ReadAsStringAsync();
                string[] authorsList = responseBody.Split(",");
                ViewBag.ReturnMessage = "Insert Successful via " + authorsList[1] + " RecordId: " + authorsList[0];
            }
            catch (Exception ex)
            {
                ViewBag.ReturnMessage = ex.Message;
            }

            return View("Index");
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
    }
}
