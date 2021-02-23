using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Models
{
    public class TransactionDTOReqCSV
    {        
        public string SenderVID { get; set; }
        public string ReceiverVID { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Amount { get; set; }
        public string SenderBankSwiftCode { get; set; }
        public string ReceiverBankSwiftCode { get; set; }
        public string ChannelName { get; set; }
        public string DeviceID { get; set; }
        public string IDTPPIN { get; set; }
        public string EndToEndID { get; set; }
        public string PaymentNote { get; set; }
        public string MessageID { get; set; }
        public string TransactionId { get; set; }
        public int TransactionTypeId { get; set; }
        public string SendingBankTxId { get; set; }
        public string MobileNumber { get; set; }
        public string LatLong { get; set; }
        public string IPAddress { get; set; }
        public string SenderAccNo { get; set; }
        public string ReceiverAccNo { get; set; }
        public string SendingBankRoutingNo { get; set; }
        public string ReceivingBankRoutingNo { get; set; }
        public string ReferenceIDTP { get; set; }
        public string ReferernceNumber { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public int SenderBankId { get; set; }
        public int ReceiverBankId { get; set; }
        public string SendingBankReference { get; set; }
        public string SendingPSPReference { get; set; }
        public string ReceivingBankReference { get; set; }
        public string ReceivingPSPReference { get; set; }
        public string FeeAmount { get; set; }
        public string VATAmount { get; set; }
        public string SendingBankCBAccountNo { get; set; }
        public string ReceivingBankCBAccountNo { get; set; }
        public bool IsVATApplicable { get; set; }
        public bool IsRTPDirectPay { get; set; }
        public string ActualAmount { get; set; }
        public string AmountWithoutFee { get; set; }
        public int SenderRoleId { get; set; }
        public int SenderFinancialInstitutionType { get; set; }
        public int ReceiverFinancialInstitutionType { get; set; }
        public long IndirectParticipantSender { get; set; }
        public string IndirectParticipantSenderAccount { get; set; }
        public long IndirectParticipantReceiver { get; set; }
        public string IndirectParticipantReceiverAccount { get; set; }
        public int IsIndirectPartipantTransaction { get; set; }
        public string InvoiceNo { get; set; }
    }
}
