using Helper.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IDTPApp.Controllers
{
    public class DirectPayController : Controller
    {
        public async void DirectPay() {

            using (HttpClient client = new HttpClient()) {
                StringContent content = new StringContent(JsonConvert.SerializeObject(new Payload()), Encoding.UTF8, "application/json");
                
                string endpoint = "localhost:3344" + "/TransactionFundInsertBaseLine";
                using (var Response = await client.PostAsync(endpoint, content)) {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK) {
                   
                    }
                    else {
                    }
                }

            }

        }
    }
}
