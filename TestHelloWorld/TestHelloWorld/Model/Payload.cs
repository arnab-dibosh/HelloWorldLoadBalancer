using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestHelloWorld.Model
{
    public class Payload
    {
        public string xmlData { get; set; }
        public string transactionId { get; set; }
        public string clientRequestTime { get; set; }
    }

    public class SimplePayload
    {
        public string transactionId { get; set; }
        public string clientRequestTime { get; set; }
    }
}
