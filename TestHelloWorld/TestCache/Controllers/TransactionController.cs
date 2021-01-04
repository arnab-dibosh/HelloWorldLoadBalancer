using Helper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helper.Models;
using Helper;

namespace TestCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly IIDTPCache _idtpCache;
        ITransactionCache _transactionCache;
        public TransactionController(ITransactionCache transactionCache, IIDTPCache idtpCache) {

            _transactionCache = transactionCache;
            _idtpCache = idtpCache;
        }

        [HttpPost("/PopulateUser", Name = "PopulateUser")]
        public string PopulateUser() {
            try {

                List<User> UserList = DBUtility.GetAllUser();

                foreach (var user in UserList) {
                    _transactionCache.UserDictionary.Add(user.VirtualID, user);
                }

                return $"{UserList.Count} User populated";
            }
            catch (Exception e) {
                return e.Message;
            }
        }

        [HttpPost("/TransactionFundInsertOnlyCSV", Name = "TransactionFundInsertOnlyCSV")]
        public string TransactionFundInsertOnlyCSV([FromBody] Payload payload) {
            try {

                var serviceRequestDTO = new TransactionDTOReq();
                var payLoads = payload.xmlData.Split(",").Select(x => x.Trim()).ToArray();
                serviceRequestDTO.SenderBankSwiftCode = payLoads[0];
                serviceRequestDTO.SenderVID = payLoads[1];
                serviceRequestDTO.ReceiverVID = payLoads[2];
                //serviceRequestDTO.Amount = payLoads[3];
                //serviceRequestDTO.ChannelName = payLoads[4];
                //serviceRequestDTO.DeviceID = payLoads[5];
                //serviceRequestDTO.MobileNumber = payLoads[6];
                //serviceRequestDTO.LatLong = payLoads[7];
                //serviceRequestDTO.IPAddress = payLoads[8];
                //serviceRequestDTO.IDTPPIN = payLoads[9];
                //serviceRequestDTO.EndToEndID = payLoads[10];
                //serviceRequestDTO.SendingBankTxId = payLoads[11];
                //serviceRequestDTO.ReferenceSendingBANK = payLoads[12];
                //serviceRequestDTO.MessageID = payLoads[13];

                User sender, receiver;
                bool senderExists = _transactionCache.UserDictionary.TryGetValue(serviceRequestDTO.SenderVID, out sender);
                bool receiverExists = _transactionCache.UserDictionary.TryGetValue(serviceRequestDTO.ReceiverVID, out receiver);

                if (senderExists && receiverExists)
                    _idtpCache.SetValue(payload.xmlData);

                return "Write in memory successfull";
            }
            catch (Exception e) {
                return e.Message;
            }
        }       

    }

    
}
