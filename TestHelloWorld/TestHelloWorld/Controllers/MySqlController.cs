using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using TestHelloWorld.Model;

namespace TestHelloWorld.Controllers
{
    [ApiController]
    public class MySqlController : ControllerBase
    {
        public MySqlController() {

        }

      
        [HttpPost("/WriteDataPayloadMySql", Name = "WriteDataPayloadMySql")]
        public string WriteDataPayloadMySql([FromBody] SimplePayload payload) {
            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try {
                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try {
                    MysqlDbUtility.WriteData(payload.transactionId, "Hello-World", strDockerName, payload.clientRequestTime, apiRequestStartTime);
                }
                catch (Exception) {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }

    }
}
