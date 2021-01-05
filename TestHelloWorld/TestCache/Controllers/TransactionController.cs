using Helper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helper;

namespace TestCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly IIDTPTransCache _idtpTransCache;
        IMasterDataCache _masterDataCache;
        public TransactionController(IMasterDataCache masterCache, IIDTPTransCache idtpCache) {

            _masterDataCache = masterCache;
            _idtpTransCache = idtpCache;
        }

        [HttpPost("/PopulateMasterData", Name = "PopulateMasterData")]
        public string PopulateMasterData() {
            try {

                return IDTPUtility.LoadMasterData(_masterDataCache, false);
            }
            catch (Exception e) {
                return e.Message;
            }
        }

        [HttpGet("/GetUser", Name = "GetUser")]
        public User GetUser(string vid) {
            try {
                var user = _masterDataCache.UserDictionary[vid];
                return user;
            }
            catch (Exception e) {
                throw e;
            }
        }

        [HttpGet("/GetUser", Name = "GetUser")]
        public UserAccountInformationDTO GetAccounInfo(long id) {
            try {
                var accInfo = _masterDataCache.FiDictionary[id];
                return accInfo;
            }
            catch (Exception e) {
                throw e;
            }
        }

        [HttpGet("/ShowMasterData", Name = "ShowMasterData")]
        public string ShowMasterData() {
            try {

                return $" User count: {_masterDataCache.UserDictionary.Count}, Fi count: {_masterDataCache.FiDictionary.Count}";
            }
            catch (Exception e) {
                return e.Message;
            }
        }

        [HttpPost("/TransactionFundInsertOnlyCSV", Name = "TransactionFundInsertOnlyCSV")]
        public string TransactionFundInsertOnlyCSV([FromBody] Payload payload) {
            try {

                var tranDto = new TransactionDTOReq();
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
                tranDto.ReferenceSendingBANK = payLoads[12];
                tranDto.MessageID = payLoads[13];

                string IdtpRef = "IDTP" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                User sender, receiver;
                bool senderExists = _masterDataCache.UserDictionary.TryGetValue(tranDto.SenderVID, out sender);
                bool receiverExists = _masterDataCache.UserDictionary.TryGetValue(tranDto.ReceiverVID, out receiver);
                if (!senderExists) throw new Exception("Invalid Sender");
                if (!receiverExists) throw new Exception("Invalid Receiver");
                if(!SecurityService.DecryptAndCheck(tranDto.IDTPPIN, sender.SecretSalt, sender.IDTP_PIN)) throw new Exception("Invalid IDTP PIN");

                UserAccountInformationDTO senderAccInfo, receiverAccInfo;
                _masterDataCache.FiDictionary.TryGetValue(sender.DefaultFI, out senderAccInfo);
                _masterDataCache.FiDictionary.TryGetValue(receiver.DefaultFI, out receiverAccInfo);

                if (tranDto.ChannelName == "Mobile" && tranDto.DeviceID != senderAccInfo.DeviceID)
                    throw new Exception("Invalid User Device Id");


                tranDto.SenderAccNo = senderAccInfo.AccountNumber;
                tranDto.ReceiverAccNo = receiverAccInfo.AccountNumber;
                tranDto.SendingBankRoutingNo = "123456";
                tranDto.ReceivingBankRoutingNo = "123456";
                tranDto.SenderBankId = senderAccInfo.FinancialInstitutionId;
                tranDto.ReceiverBankId = receiverAccInfo.FinancialInstitutionId;

                tranDto.PaymentNote = "Payment Note";
                tranDto.ReferenceIDTP = IdtpRef;
                tranDto.TransactionTypeId = "1";
                tranDto.ReceivingBankReference = "";
                tranDto.SenderId = sender.UserId;
                tranDto.ReceiverId = receiver.UserId;

                _idtpTransCache.SetTransValue(tranDto);

                return "Write in memory successfull";
            }
            catch (Exception e) {
                return e.Message;
            }
        }

    }


}
