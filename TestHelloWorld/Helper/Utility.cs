using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class Utility
    {
        public static TransactionDTOReqCSV CreateTransactionDTOFromCSV(string csvData) {
            var tranDto = new TransactionDTOReqCSV();
            var payLoads = csvData.Split(",").Select(x => x.Trim()).ToArray();
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
            tranDto.MessageID = payLoads[12];
            tranDto.SendingBankReference = payLoads[13];
            tranDto.PaymentNote = payLoads[14];
            tranDto.InvoiceNo = payLoads[15];
            return tranDto;
        }
    }
}
