using Helper.Models;
using System.Collections.Generic;

namespace TestCache
{
    public interface IMasterDataCache
    {
        Dictionary<string, User> UserDictionary { get; set; }
        Dictionary<long, UserAccountInformationDTO> FiDictionary { get; set; }
    }
}