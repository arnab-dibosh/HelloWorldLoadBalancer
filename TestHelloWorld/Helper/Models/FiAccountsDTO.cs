using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Models
{
    public class FiAccountsDTO
    {
        public long FiAccId { get; set; }
        public long FiInstId { get; set; }
        public string AccNum { get; set; }
        public string DeviceId { get; set; }
        public string SwiftBic { get; set; }
        public long BankId { get; set; }
        public int FiInstType { get; set; }
        public long? IndPartcId { get; set; }
        public string IndPartcAccNum { get; set; }
    }
}
