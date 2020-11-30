using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Xml;
using System.Net;
using TestHelloWorld.Model;
using System.Security.Cryptography;
using Cryptography.Services;

namespace TestHelloWorld.Controllers
{
    [ApiController]
    public class BinaryController : ControllerBase
    {
        private readonly ILogger<DynamicSqlController> _logger;        
        public BinaryController()
        {
        }
       
        [HttpPost("/SerializeXMLWithBinaryFormatter", Name = "SerializeXMLWithBinaryFormatter")]
        public string SerializeXMLWithBinaryFormatter([FromBody] string xmlData) {
            string resultString = "Serialized Successfully";   
            try {
               Helper.Serialize(xmlData);
            }
            catch (Exception ex) {
                resultString = ex.Message;
            }
            return resultString;
        }

    }
}
