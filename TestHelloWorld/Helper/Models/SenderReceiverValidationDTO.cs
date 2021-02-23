using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Models
{
    public class SenderReceiverValidationDTO
    {
        public string SenderVirtualId { get; set; }
        public string ChannelName { get; set; }
        public string DeviceID { get; set; }
        public decimal Amount { get; set; }
        public string SwiftBic { get; set; }
        public int ReturnedErrorCode { get; set; }
        public string IDTPPIN { get; set; }
        public string SecretSalt { get; set; }
        public decimal DailySendingTransactionAmount { get; set; }

        #region
        public int SenderRoleId { get; set; }
        public long SenderId { get; set; }
        public int SenderBankId { get; set; }
        public string SenderAccNo { get; set; }
        public int SenderFinancialInstitutionType { get; set; }
        public long? IndirectParticipantSender { get; set; }
        public string IndirectParticipantSenderAccount { get; set; }
        public int SenderFiInstType { get; set; }
        #endregion

        #region Receiver
        public int ReceiverRoleId { get; set; }
        public string ReceiverVirtualId { get; set; }
        public long ReceiverId { get; set; }
        public int ReceiverBankId { get; set; }
        public string ReceiverAccNo { get; set; }
        public int ReceiverFinancialInstitutionType { get; set; }
        public decimal DailyReceivingTransactionAmount { get; set; }
        public string ReceiverBankSwiftCode { get; set; }
        public long? IndirectParticipantReceiver { get; set; }
        public string IndirectParticipantReceiverAccount { get; set; }
        public int ReceiverFiInstType { get; set; }
        #endregion
    }
}
