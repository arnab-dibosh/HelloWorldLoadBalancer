using Helper;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCache
{
    public class IDTPUtility
    {
        

        public static string LoadMasterData(IMasterDataCache masterDataCache, bool takeModifiedOnly) {

            List<User> UserList = DBUtility.GetAllUser(takeModifiedOnly);
            List<UserAccountInformationDTO> userAccounts = DBUtility.GetAllUserAccountInfo(takeModifiedOnly);
            foreach (var user in UserList) {
                masterDataCache.UserDictionary[user.VirtualID]= user;
            }

            foreach (var accInfo in userAccounts) {
                masterDataCache.FiDictionary[accInfo.Id] = accInfo;
            }

            return $"{UserList.Count} User {userAccounts.Count} FI populated";
        }
    }
}
