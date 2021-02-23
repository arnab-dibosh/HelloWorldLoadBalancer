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


        public static string ProcessDirectPay(TransactionDTOReqCSV transactionDtoReq)
        {
            try
            {
                
                transactionDtoReq = SenderReceiverValidation(transactionDtoReq);

                SqlDBUtility.InsertTransaction(transactionDtoReq);

                return "Transaction Inserted";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private static TransactionDTOReqCSV SenderReceiverValidation(TransactionDTOReqCSV transactionDtoReq)
        {
     
            User sender = SqlDBUtility.GetUserByVid(transactionDtoReq.SenderVID);

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

            List<FiAccountsDTO> senderItems = JsonConvert.DeserializeObject<List<FiAccountsDTO>>(sender.FiUserData);
            var senderData = senderItems.FirstOrDefault(x => x.FiInstId == 11); // Need Bank Id

            transactionDtoReq.SenderId = (senderData.FiInstType == 2) ? senderData.FiInstId: sender.UserId;
            transactionDtoReq.SenderAccNo = senderData.AccNum;
            transactionDtoReq.SenderBankId = Convert.ToInt32(senderData.BankId);
            transactionDtoReq.IndirectParticipantSender = Convert.ToInt64(senderData.IndPartcId);
            transactionDtoReq.IndirectParticipantSenderAccount = senderData.IndPartcAccNum;

            // Get Receiver Information
            User receiver = SqlDBUtility.GetUserByVid(transactionDtoReq.ReceiverVID);
            // Check Receiver Exist
            if (string.IsNullOrEmpty(receiver.VirtualID))
                throw new Exception("Receiver does not exist");

            // Check Is Receiver Restricted
            if (receiver.IsRestricted == 1)
                throw new Exception("Receiver is Restricted");

            // Check Is Receiver Device Restricted
            if (receiver.IsDeviceRestricted == 1)
                throw new Exception("Receiver Device is Restricted");

            List<FiAccountsDTO> receiverItems = JsonConvert.DeserializeObject<List<FiAccountsDTO>>(receiver.FiUserData);
            var receiverData = receiverItems.FirstOrDefault(x => x.FiAccId == receiver.DefaultFI); 

            transactionDtoReq.ReceiverId = (receiverData.FiInstType == 2) ? receiverData.FiInstId : receiver.UserId;
            transactionDtoReq.ReceiverAccNo = receiverData.AccNum;
            transactionDtoReq.ReceiverBankId = Convert.ToInt32(receiverData.BankId);
            transactionDtoReq.IndirectParticipantReceiver = Convert.ToInt64(receiverData.IndPartcId);
            transactionDtoReq.IndirectParticipantReceiverAccount = receiverData.IndPartcAccNum;
            transactionDtoReq.ReceiverBankSwiftCode = receiverData.SwiftBic;
            transactionDtoReq.IsIndirectPartipantTransaction = ((senderData.FiInstType == 2) || (receiverData.FiInstType == 2)) ? 1 : 0;

            return transactionDtoReq;
        }




    }
}
