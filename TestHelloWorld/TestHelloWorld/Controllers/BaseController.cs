using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Xml;
using System.Net;
using TestHelloWorld.Model;

//using StackExchange.Redis;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestHelloWorld.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        private readonly ILogger<BaseController> _logger;
        private readonly TestDbContext _context;

        public BaseController(ILogger<BaseController> logger, TestDbContext context)
        {
            _context = context;
            _logger = logger;
            ////_logger.LogDebug(1, "NLog injected into HomeController");
        }

        //public IActionResult Index()
        //{
        //    _logger.LogInformation("Hello, this is the index!");
        //    return View();
        //}

        // GET: api/<BaseController>
       

        private static readonly string[] Names = new[]
        {
            "Arnab", "Nesar", "Sumon"
        };


        //public UtilityController() {
        //}

        [HttpGet("/GetData", Name = "GetData")]
        public IEnumerable<string> GetData()
        {
            _logger.LogDebug(1, "Start..");
            return Names;
        }

        [HttpPost("/WriteData", Name = "WriteData")]

        public string WriteData(string transactionId, string clientRequestTime)
        {
            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
            _logger.LogDebug(1, transactionId + "|Req- " + apiRequestStartTime);
            string retVal = transactionId;
            try
            {
                //string strConnectionString = ReadConfigurationValue("DBConnectionString");

                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;

                //_logger.LogDebug(2, transactionId + "|BT- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                try
                {
                    //DBUtility.ConnectionString = "Data Source = NESAR-2019\\SQL2017;Initial Catalog=LoadBalancer;Integrated Security=true;Pooling=true;Max Pool Size=300;";
                    DBUtility.ConnectionString = "Server=192.168.1.33;Database=LoadBalancer;User ID=sa;password=Techvision123?;Pooling=true;Max Pool Size=300;";
                    //TIME 111
                    //_logger.LogDebug(3, transactionId + "|AC- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                    //Task.Delay(3);
                    DBUtility.InsertTable(transactionId, "Hello-World", strDockerName, clientRequestTime, apiRequestStartTime, _logger);
                    /*if (DBUtility.InsertTable(transactionId, "Hello-World", strDockerName, clientRequestTime, apiRequestStartTime))
                    {
                        dtApiResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"); //TIME 222
                        DBUtility.InsertApiResponseTimeTable(transactionId, dtApiResponseTime);
                    }*/
                    dtApiResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"); //TIME 222
                    _logger.LogDebug(8, transactionId + "|Res- " + dtApiResponseTime);
                }
                catch (Exception ex)
                {
                    DBUtility.InsertExceptionLogTable(transactionId, ex.Message.Replace("'", "''"), Environment.MachineName,
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                    throw;
                }

                return retVal;
            }
            catch (Exception)
            {
                //return transactionId + " - Error: " + ex.Message;
                throw;
            }
        }


        [HttpPost("/DirectPayRequest", Name = "DirectPayRequest")]
        public IActionResult DirectPayRequest(Payload payload, string transactionId, string clientRequestTime)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(payload.xmlData);
            try
            {
                var accountNumber = doc.GetElementsByTagName("DbtrAcct").Item(0).InnerText;
                var amount = doc.GetElementsByTagName("IntrBkSttlmAmt").Item(0).InnerText;

                //DBUtility.ConnectionString = "Data Source = NESAR-2019\\SQL2017;Initial Catalog=LoadBalancer;Integrated Security=true;Pooling=true;Max Pool Size=300;";
                DBUtility.ConnectionString = "Server=192.168.1.33;Database=LoadBalancer;User ID=sa;password=Techvision123?;Pooling=true;Max Pool Size=300;";

                DBUtility.InsertTransactionsTable(accountNumber, amount, transactionId, clientRequestTime, _logger);
                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Request Submitted Successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
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
