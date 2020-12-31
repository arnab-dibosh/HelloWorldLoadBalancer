using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using TestHelloWorld.Model;
using Microsoft.Extensions.Caching.Memory;
using System.IO;
using System.Text;

namespace TestHelloWorld.Controllers
{
    [ApiController]
    public class CacheController : ControllerBase
    {
        public CacheController() {

        }

        [HttpPost("/WriteInBuffer", Name = "WriteInBuffer")]
        public string WriteInBuffer([FromBody] PayloadXml payload) {
          
            try {
                using (var stream = new MemoryStream()) {
                    var sw = new StreamWriter(stream);
                    sw.Write(payload.xmlData);

                    sw.Flush();
                    stream.Position = 0;

                    //using (var streamReader = new StreamReader(stream)) {
                    //    var output= streamReader.ReadToEnd();
                    //}
                }
            }
            catch (Exception ex) {

                return ex.Message;
            }

            return $"Written data In Buffer";
        }

    }
}
