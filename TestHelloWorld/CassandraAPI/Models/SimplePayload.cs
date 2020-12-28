using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraAPI.Models
{
    public class SimplePayload
    {
        public string transactionId { get; set; }
        public string clientRequestTime { get; set; }
    }
}
