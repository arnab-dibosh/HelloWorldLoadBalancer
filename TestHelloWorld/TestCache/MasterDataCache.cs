using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Helper.Models;

namespace TestCache
{
    public class MasterDataCache : IMasterDataCache
    {

        public Dictionary<long, UserAccountInformationDTO> _accInfoDic = new Dictionary<long, UserAccountInformationDTO>();
        public Dictionary<string, User> _userDictionary = new Dictionary<string, User>();

        public Dictionary<string, User> UserDictionary { 
            get => _userDictionary; 
            set => _userDictionary=UserDictionary; 
        }
        public Dictionary<long, UserAccountInformationDTO> FiDictionary { 
            get => _accInfoDic; 
            set => _accInfoDic=FiDictionary; 
        }
    }
}
