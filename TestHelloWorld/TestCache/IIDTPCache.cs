using Helper.Models;
using System.Collections.Generic;

namespace TestCache
{
    public interface IIDTPCache
    {
        void SetValue(string value);

        IEnumerable<string> GetCollection();

    }

    public interface IIDTPTransCache
    {
        void SetTransValue(TransactionDTOReq value);

        IEnumerable<TransactionDTOReq> GetTransCollection();

    }
}