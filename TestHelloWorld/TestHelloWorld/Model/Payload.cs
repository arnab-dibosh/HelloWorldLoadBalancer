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

    public class BiteArrayPayload
    {
        public byte[] binaryData { get; set; }
    }

    public class PayloadXml
    {
        public string xmlData { get; set; }
    }

    public class SucurityPayload
    {
        public string xmlData { get; set; }
        public string signatureData { get; set; }
    }
    public class SucurityPayloadAll
    {
        public string key32Bytes { get; set; }
        public string nonce12Bytes { get; set; }
        public string assocData { get; set; }
        public string plainText { get; set; }
        public string signatureData { get; set; }
    }
}
