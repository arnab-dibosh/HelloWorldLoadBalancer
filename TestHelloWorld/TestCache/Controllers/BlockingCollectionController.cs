using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace TestCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlockingCollectionController  : ControllerBase
    {
        private readonly ILogger<BlockingCollectionController> _logger;
        private readonly IMemoryCache _memcache;
        private readonly IIDTPCache _idtpCache;

        public BlockingCollectionController(ILogger<BlockingCollectionController> logger, IIDTPCache idtpCache)
        {
            _logger = logger;
            _idtpCache = idtpCache;
          
        }

        

        [HttpPost("/InsertBlockingCollection", Name = "InsertBlockingCollection")]
        public string InsertBlockingCollection() {
            _idtpCache.SetValue("TEST:" + DateTime.Now.ToString());
            return "cache set";
        }
    }
}
