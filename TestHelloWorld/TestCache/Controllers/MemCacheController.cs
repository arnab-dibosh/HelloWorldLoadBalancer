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
    public class MemCacheController : ControllerBase
    {
        private readonly IMemoryCache _memcache;

        public MemCacheController(IMemoryCache memoryCache)
        {
            _memcache = memoryCache;
        }

        [HttpPost("/InsertIMemoryCache", Name = "InsertIMemoryCache")]
        public string InsertIMemoryCache()
        {
            _memcache.GetOrCreate(DateTime.Now.ToString(), entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(3);
                return DateTime.Now.ToString();
            });
            return "cache set";
        }

    }
}
