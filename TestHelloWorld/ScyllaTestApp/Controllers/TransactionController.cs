using Helper.Models;
using Helper;
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
        public string Transferfundcsvwithscylla([FromBody] CSVPayload payload) {

            try {
                TransactionDTOReqCSV transactionDTO = Utility.CreateTransactionDTOFromCSV(payload.CsvData);

               var returnMsg= Utility.ProcessDirectPay(transactionDTO);      

                return returnMsg;
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }

    }

}
