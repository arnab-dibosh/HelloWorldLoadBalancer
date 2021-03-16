using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class HomeController : Controller
    {

        public HomeController() {

        }

        public IActionResult Index() {

            var machineName = Environment.MachineName;
            ViewBag.MachineName = machineName;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult DirectPay() {

            var machineName = Environment.MachineName;
            ViewBag.MachineName = machineName;
            try {
                DBUtility.WriteData();
                ViewBag.ReturnMessage = "Insert Successfull";
            }
            catch (Exception ex) {
                ViewBag.ReturnMessage = ex.Message;
            }
            
            return View("Index");
        }
    }
}
