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

        private readonly ILogger<BaseController> _logger;
        private readonly TestDbContext _context;

        public DirectPayController(ILogger<BaseController> logger, TestDbContext context) {
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
                string amountstr = doc.GetElementsByTagName("IntrBkSttlmAmt").Item(0).InnerText;

                if (!idtppin.Equals("123456")) throw new Exception("Invalid Pin");

                decimal amount = 0;
                decimal.TryParse(amountstr, out amount);

                var sernderInfo = _context.Users.Include(s => s.Bank).Where(x => x.VID == sendervid).FirstOrDefault();
                var receiverInfo = _context.Users.Include(s => s.Bank).Where(x => x.VID == receiverVid).FirstOrDefault();
                decimal availBalance = 0;
                

                if (sernderInfo.Bank.Name == "SampleBank1") {
                    availBalance = _context.SampleBank1Info.Where(x => x.AccountNo == sernderInfo.AccountNo).FirstOrDefault().Balance;
                }
                else if (sernderInfo.Bank.Name == "SampleBank2") {
                    availBalance = _context.SampleBank2Info.Where(x => x.AccountNo == sernderInfo.AccountNo).FirstOrDefault().Balance;
                }
                if (amount > availBalance) throw new Exception("Does not have enoungh balance!");

                var transaction = new Transaction();
                transaction.TransactionId = transactionId;//Guid.NewGuid().ToString();
                transaction.SenderVid = sernderInfo.VID;
                transaction.SenderAccNo = sernderInfo.AccountNo;
                transaction.SenderBankId = sernderInfo.BankId;
                transaction.ReceicerVid = receiverInfo.VID;
                transaction.ReceiverAccNo = receiverInfo.AccountNo;
                transaction.ReceicerBankId = receiverInfo.BankId;
                transaction.Amount = amount;
                transaction.TranDate = DateTime.Now;
                transaction.ClientRequestTime = clientRequestTime;

                _context.Transactions.Add(transaction);

                //if (sernderInfo.Bank.Name == "SampleBank1") {
                //    var senderBankInfo = _context.SampleBank1Info.Where(x => x.AccountNo == sernderInfo.AccountNo).FirstOrDefault();
                //    senderBankInfo.Balance -= amount;
                //    _context.SampleBank1Info.Update(senderBankInfo);

                //}
                //else if (sernderInfo.Bank.Name == "SampleBank2") {
                //    var senderBankInfo = _context.SampleBank2Info.Where(x => x.AccountNo == sernderInfo.AccountNo).FirstOrDefault();
                //    senderBankInfo.Balance -= amount;
                //    _context.SampleBank2Info.Update(senderBankInfo);
                //}

                //if (receiverInfo.Bank.Name == "SampleBank1") {
                //    var receiverBankInfo = _context.SampleBank1Info.Where(x => x.AccountNo == receiverInfo.AccountNo).FirstOrDefault();
                //    receiverBankInfo.Balance += amount;
                //    _context.SampleBank1Info.Update(receiverBankInfo);
                //}
                //else if (receiverInfo.Bank.Name == "SampleBank2") {
                //    var receiverBankInfo = _context.SampleBank2Info.Where(x => x.AccountNo == receiverInfo.AccountNo).FirstOrDefault();
                //    receiverBankInfo.Balance += amount;
                //    _context.SampleBank2Info.Update(receiverBankInfo);
                //}
                _context.SaveChanges();

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
