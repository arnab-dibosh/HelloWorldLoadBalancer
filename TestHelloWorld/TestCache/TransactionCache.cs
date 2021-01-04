using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Helper.Models;

namespace TestCache
{
    public class TransactionCache : ITransactionCache
    {

        public Dictionary<string, User> _serDictionary = new Dictionary<string, User>();

        public Dictionary<string, User> UserDictionary { 
            get => _serDictionary; 
            set => _serDictionary=UserDictionary; 
        }
    }
}
