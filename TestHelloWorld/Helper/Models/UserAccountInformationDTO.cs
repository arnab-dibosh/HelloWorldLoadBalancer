using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Models
{
    public class UserAccountInformationDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int FinancialInstitutionId { get; set; }
        public int FinancialInstitutionType { get; set; }
        public int BranchId { get; set; }
        public string AccountNumber { get; set; }
        public string DeviceID { get; set; }
        public bool IsClosed { get; set; }
        public bool IsEnabled { get; set; }
        public int IsLoaded { get; set; }
    }
}
