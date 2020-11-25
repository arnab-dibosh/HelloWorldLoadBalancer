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
using Microsoft.EntityFrameworkCore;

//using StackExchange.Redis;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestHelloWorld.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DirectPayController : ControllerBase
    {

        private readonly ILogger<DirectPayController> _logger;
        private readonly TestDbContext _context;

        public DirectPayController(ILogger<DirectPayController> logger, TestDbContext context) {
            _context = context;
            _logger = logger;
        }


        [HttpPost("/DirectPay", Name = "DirectPay")]
        public IActionResult DirectPay(Payload payload, string transactionId, string clientRequestTime) {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(payload.xmlData);
            try {
                string sendervid = "", receiverVid = "", idtppin="";

                sendervid = doc.GetElementsByTagName("DbtrAcct").Item(0).InnerText;
                receiverVid = doc.GetElementsByTagName("CdtrAcct").Item(0).InnerText;
                idtppin = doc.GetElementsByTagName("IDTP_PIN").Item(0).InnerText;
                string amountstr = "";
                amountstr = doc.GetElementsByTagName("IntrBkSttlmAmt").Item(0).InnerText;

                if (!idtppin.Equals("123456")) throw new Exception("Invalid Pin");

                decimal amount = 0;
                decimal.TryParse(amountstr, out amount);

                 decimal availBalance = 0;
                DBUtility.ConnectionString = "Server=DESKTOP-QS1VJGL\\SQLEXPRESS;Database=TestHelloWorld;User ID=sa;password=bs23;";
                //DBUtility.ConnectionString = "Server=192.168.1.33;Database=TestHelloWorld;User ID=sa;password=Techvision123?;Pooling=true;Max Pool Size=300;";
                DBUtility.ProcessDirectPay(sendervid, receiverVid, amount, transactionId, clientRequestTime, _logger);


                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex) {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpGet("/TestOperation", Name = "TestOperation")]
        public IEnumerable<Bank> TestOperation() {
            var banks = _context.Banks.ToList();
            return banks;
        }


    }
}
