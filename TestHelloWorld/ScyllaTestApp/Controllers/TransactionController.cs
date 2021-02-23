using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScyllaTestApp.Controllers
{
    [ApiController]
    public class TransactionController : Controller
    {
        [HttpPost("/Transferfundcsvwithscylla", Name = "Transferfundcsvwithscylla")]
        public string Transferfundcsvwithscylla([FromBody]Payload payload) {
            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try {

                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try {
                    
                }
                catch (Exception) {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception) {
                throw;
            }
        }

    }

    public class Payload
    {
        public string CsvData { get; set; }
    }
}
