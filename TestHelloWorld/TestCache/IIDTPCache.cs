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
        void SetTransValue(string value);

        IEnumerable<string> GetTransCollection();

    }
}