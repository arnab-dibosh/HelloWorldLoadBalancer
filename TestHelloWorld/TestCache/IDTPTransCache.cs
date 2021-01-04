using Helper.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCache
{
    public class IDTPTransCache : IIDTPTransCache
    {
        private BlockingCollection<TransactionDTOReq> _blockingTransCollection = new BlockingCollection<TransactionDTOReq>();
        public IEnumerable<TransactionDTOReq> GetTransCollection()
        {
            return _blockingTransCollection.GetConsumingEnumerable();
        }

        public void SetTransValue(TransactionDTOReq value)
        {
            _blockingTransCollection.Add(value);
        }
    }
}
