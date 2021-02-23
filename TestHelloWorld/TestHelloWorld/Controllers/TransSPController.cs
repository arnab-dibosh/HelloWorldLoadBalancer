using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHelloWorld.Model;

namespace TestHelloWorld.Controllers
{
    [ApiController]
    public class TransSPController : ControllerBase
    {
        [HttpPost("/TransactionInsertBaseLine", Name = "TransactionInsertBaseLine")]
        public string TransactionInsertBaseLine([FromBody] Payload payload)
        {
            try
            {

                var tranDto = new TransactionDTO();
                var payLoads = payload.xmlData.Split(",").Select(x => x.Trim()).ToArray();
                tranDto.SenderBankSwiftCode = payLoads[0];
                tranDto.SenderVID = payLoads[1];
                tranDto.ReceiverVID = payLoads[2];
                tranDto.Amount = payLoads[3];
                tranDto.ChannelName = payLoads[4];
                tranDto.DeviceID = payLoads[5];
                tranDto.MobileNumber = payLoads[6];
                tranDto.LatLong = payLoads[7];
                tranDto.IPAddress = payLoads[8];
                tranDto.IDTPPIN = payLoads[9];
                tranDto.EndToEndID = payLoads[10];
                tranDto.SendingBankTxId = payLoads[11];
                tranDto.SendingBankReference = payLoads[12];
                tranDto.MessageID = payLoads[13];

                string IdtpRef = "IDTP" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff");

                tranDto.SenderAccNo = "ACC123";
                tranDto.ReceiverAccNo = "ACC345";
                tranDto.SendingBankRoutingNo = "123456";
                tranDto.ReceivingBankRoutingNo = "123456";
                tranDto.SenderBankId = 1;
                tranDto.ReceiverBankId = 2;

                tranDto.PaymentNote = "Payment Note";
                tranDto.ReferenceIDTP = IdtpRef;
                tranDto.TransactionTypeId = "1";
                tranDto.SenderId = 1;
                tranDto.ReceiverId = 2;
                tranDto.FeeAmount = "0";
                tranDto.VATAmount = "0";
                tranDto.SendingPSPReference = "";
                tranDto.ReceivingBankReference = "";
                tranDto.ReceivingPSPReference = "";

                DBUtility.TransactionSp(tranDto);

                return "Insert successfull";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
