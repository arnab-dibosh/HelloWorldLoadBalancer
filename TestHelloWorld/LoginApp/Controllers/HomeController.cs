using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDTPApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult InsertInHelloWorld() {
            try {
                

            }
            catch (Exception ex) {

                ViewBag.Result = ex.Message;
            }
            return View("Index");
        }
    }
}
