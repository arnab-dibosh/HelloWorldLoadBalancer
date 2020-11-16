using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace HelloWorldLoadBalacer.Controllers
{
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private static readonly string[] Names = new[]
        {
            "Arnab", "Nesar", "Sumon"
        };
        
        public UtilityController() {
        }

        [HttpGet("/GetData", Name = "GetData")]
        public IEnumerable<string> GetData() {
            return Names;
        }

        [HttpPost("/WriteData", Name = "WriteData")]
        public string WriteData(string transactionId, string clientRequestTime)
        {
            string retVal = transactionId;
            try
            {
                string apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                string strConnectionString = ReadConfigurationValue("DBConnectionString");

                string strDockerName = System.Environment.MachineName;
                string[] ConnectionStringList = strConnectionString.Split('|');
                string dtApiResponseTime = string.Empty;
                

                foreach (string item in ConnectionStringList)
                {
                    try
                    {
                        DBUtility.ConnectionString = item.Trim();
                        apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"); //TIME 111

                        if (DBUtility.InsertTable(transactionId, "Hello-World", strDockerName, clientRequestTime, apiRequestStartTime))
                        {
                            dtApiResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"); //TIME 222
                            DBUtility.InsertApiResponseTimeTable(transactionId, dtApiResponseTime);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        DBUtility.InsertExceptionLogTable(transactionId, ex.Message, Environment.MachineName,
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                    }
                }

                if (!string.IsNullOrEmpty(dtApiResponseTime))
                    retVal = retVal + "|" + dtApiResponseTime + "|" + apiRequestStartTime;

                return retVal;
            }
            catch (Exception ex)
            {
                return transactionId + "Error: " + ex.Message;
            }
        }

        static IConfigurationRoot config;
        static string ReadConfigurationValue(string strKey)
        {
           

            var strVal = Environment.GetEnvironmentVariable(strKey);
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = config[strKey];
            }
            return strVal;
        }
    }
}
