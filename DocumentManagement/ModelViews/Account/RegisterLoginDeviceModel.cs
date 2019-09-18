using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ModelViews.Account
{
    public class RegisterLoginDeviceModel
    {
        public string Browser { get; set; }
        public string DeviceOS { get; set; }
        public string IPAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
