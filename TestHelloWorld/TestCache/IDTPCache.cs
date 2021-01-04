using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace TestCache
{
    public class IDTPCache : IIDTPCache
    {
        private BlockingCollection<string> _blockingCollection = new BlockingCollection<string>(); //new BlockingCollection<int>(new ConcurrentBag<int>(), 10);
        public void SetValue(string value) {
            //_value = value;

            _blockingCollection.Add(value);
        }

        public IEnumerable<string> GetCollection() {
            return _blockingCollection.GetConsumingEnumerable();
        }
    }
}
