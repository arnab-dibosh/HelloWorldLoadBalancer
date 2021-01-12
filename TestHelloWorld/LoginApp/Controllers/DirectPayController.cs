using Helper.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IDTPApp.Controllers
{
    public class DirectPayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult DirectPay() {
            try {
                var objPayloadData = new Payload();
                objPayloadData.xmlData = "SBL1BDDH,fahmid1@user.idtp,parvez@user.idtp,1.1,Online,DeviceID,01711000000,Dhaka,103.24.44.30,123456,E2Esblasd1231422, TIsblasd1231422, MSGIDsblasd1231422, REFSENBsblasd1231422";

                StringContent content = new StringContent(JsonConvert.SerializeObject(objPayloadData), Encoding.UTF8, "application/json");


                using var client = new HttpClient();
                string endpoint = "http://localhost:7001" + "/TransactionInsertBaseLine";
                using (HttpResponseMessage response = client.PostAsJsonAsync(endpoint, objPayloadData).Result) {

                    response.EnsureSuccessStatusCode();
                    var result = response.Content.ReadAsStringAsync().Result;
                    ViewBag.Result = "Transaction Successfull.";
                    
                }
            }
            catch (Exception ex) {

                ViewBag.Result = ex.Message;
            }
            return View("Index");
        }
    }
}
