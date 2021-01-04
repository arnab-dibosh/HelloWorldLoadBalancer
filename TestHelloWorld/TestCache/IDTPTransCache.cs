using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCache
{
    public class IDTPTransCache : IIDTPTransCache
    {
        private BlockingCollection<string> _blockingTransCollection = new BlockingCollection<string>();
        public IEnumerable<string> GetTransCollection()
        {
            return _blockingTransCollection.GetConsumingEnumerable();
        }

        public void SetTransValue(string value)
        {
            _blockingTransCollection.Add(value);
        }
    }
}
