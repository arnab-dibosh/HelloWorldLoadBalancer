using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloWorldLoadBalacer.Controllers
{
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private static readonly string[] Names = new[]
        {
            "Arnab", "Nesar", "Sumon"
        };

        
        public UtilityController() {
        }

        [HttpGet("/GetData", Name = "GetData")]
        public IEnumerable<string> GetData() {
            return Names;
        }

        [HttpPost("/WriteData", Name = "WriteData")]
        public string WriteData() {

            //Add write code here

            return "Write to DB sucessfull";
        }
    }
}
