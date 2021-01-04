using Helper.Models;
using System.Collections.Generic;

namespace TestCache
{
    public interface ITransactionCache
    {
        Dictionary<string, User> UserDictionary { get; set; }
    }
}