using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestHelloWorld.Model
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string SenderVid { get; set; }
        public string SenderAccNo { get; set; }
        public int SenderBankId { get; set; }
        public string ReceicerVid { get; set; }
        public string ReceiverAccNo { get; set; }
        public int ReceicerBankId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TranDate { get; set; }
    }
}
