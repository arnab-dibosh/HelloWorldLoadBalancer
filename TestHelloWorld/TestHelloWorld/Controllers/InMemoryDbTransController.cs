using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHelloWorld.Model;

namespace TestHelloWorld.Controllers
{
    [ApiController]
    public class InMemoryDbTransController : ControllerBase
    {
        [HttpPost("/TransactionInMemoryDB", Name = "TransactionInMemoryDB")]
        public string TransactionInMemoryDB([FromBody] PayloadXml payload)
        {
            try
            {
                DBUtility.TransactionInMemorySp("test");

                return "Insert successfull";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
