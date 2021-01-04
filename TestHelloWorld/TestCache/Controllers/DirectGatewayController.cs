using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DirectGatewayController : Controller
    {
        private readonly IIDTPCache _idtpCache;

        public DirectGatewayController(IIDTPCache idtpCache) {

            _idtpCache = idtpCache;
        }

        [HttpPost("/DoNothingCsv", Name = "DoNothingCsv")]
        public string DoNothingCsv([FromBody] Payload payload) {
            try {


                return "successfull";
            }
            catch (Exception e) {
                return e.Message;
            }
        }

        [HttpPost("/TransactionFundInsertOnlyCSV", Name = "TransactionFundInsertOnlyCSV")]
        public string TransactionFundInsertOnlyCSV([FromBody] Payload payload) {
            try {

                _idtpCache.SetValue(payload.xmlData);

                return "Write in memory successfull";
            }
            catch (Exception e) {
                return e.Message;
            }
        }       

    }

    public class Payload
    {
        public string xmlData { get; set; }
    }
}
