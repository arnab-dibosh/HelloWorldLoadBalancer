using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string VirtualID { get; set; }
        public string FullName { get; set; }
        public long DefaultFI { get; set; }
        public int IsLoaded { get; set; }
        public string IDTP_PIN { get; set; }        
        public string SecretSalt { get; set; }
        public string DeviceId { get; set; }
        public string ChannelName { get; set; }
        public int IsDeviceRestricted { get; set; }
        public string FiUserData { get; set; }
        

    }
}
