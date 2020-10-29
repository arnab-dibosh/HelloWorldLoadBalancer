using System;
using System.Collections.Generic;
using System.Linq;
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
        public string WriteData() {

            string retVal = "";

            string strConnectionString = ReadConfigurationValue("DBConnectionString");
            string strDockerName = System.Environment.MachineName;
            //string strDockerName = ReadConfigurationValue("DockerName");
            string[] ConnectionStringList = strConnectionString.Split('|');

            foreach (string item in ConnectionStringList)
            {
                DBUtility.ConnectionString = item.Trim();

                try
                {
                    if (DBUtility.InsertTable("Hello-World", strDockerName))
                    {
                        retVal = "Write to DB sucessfull";
                        break;
                    }
                }
                catch { }
            }

            return retVal;
        }

        static IConfigurationRoot config;
        static string ReadConfigurationValue(string strKey)
        {
            if (config == null)
            {
                config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();
            }


            var strVal = Environment.GetEnvironmentVariable(strKey);
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = config[strKey];
            }
            return strVal;
        }
    }
}
