using System.Collections.Generic;

namespace TestCache
{
    public interface IIDTPCache
    {
        void SetValue(string value);

        IEnumerable<string> GetCollection();

    }
}