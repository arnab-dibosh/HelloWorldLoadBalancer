using Helper.Models;
using Newtonsoft.Json;
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


        public static TransactionResponseDTO ProcessDirectPay(TransactionDTOReqCSV transactionDtoReq)
        {
            try
            {
                var transactionResponseDto = new TransactionResponseDTO();
                //Format Validation >>>>
                //InputDataValidation(transactionDtoReq);
                // Format Validation <<<<

                // Enrichment >>>>
                //transactionDtoReq.TransactionTypeId = IDTPUtils.Constants.TransactionTypes.DIRECT_PAY;
                var senderReceiverInfo = SenderReceiverValidation(transactionDtoReq);
                // Enrichment <<<<

                // Action >>>>
                // Process Direct Pay Transaction
                //var transactionResponseDto = ProcessTransaction(transactionDtoReq);
                // Update RTP 
                //UpdateRTPStatus(transactionDtoReq);

                return transactionResponseDto;
            }
            // Exception Handling
            catch (Exception ex)
            {
                var responseDto = new TransactionResponseDTO();
                responseDto.Message = ex.Message;
                return responseDto;
                //return responseDto;
            }
        }


        private static TransactionDTOReqCSV SenderReceiverValidation(TransactionDTOReqCSV transactionDtoReq)
        {
           
     
                User sender = ScyllaDBUtility.GetUserByVid(transactionDtoReq.SenderVID);

            // Check Sender Exist
            if (string.IsNullOrEmpty(sender.VirtualID))
                throw new Exception("Sender does not exist");

                // Check Is Sender Restricted
                if (sender.IsRestricted == 1)
                throw new Exception("Sender is Restricted");

            // Check Is Sender Device Restricted
            if (sender.IsDeviceRestricted == 1)
                throw new Exception("Sender Device is Restricted");

            //Pin Validation
            bool isValidPin = SecurityService.DecryptAndCheck(transactionDtoReq.IDTPPIN, sender.SecretSalt, sender.IDTP_PIN);
                if (!isValidPin)
                    throw new Exception("Invalid PIN");

            List<FiAccountsDTO> items = JsonConvert.DeserializeObject<List<FiAccountsDTO>>(sender.FiUserData);
            var senderData = items.FirstOrDefault(x => x.FiInstId == 11); // Need Bank Id
            transactionDtoReq.SenderAccNo = senderData.AccNum;

            User receiver = ScyllaDBUtility.GetUserByVid(transactionDtoReq.SenderVID);

           

            return transactionDtoReq;
        }




    }
}
