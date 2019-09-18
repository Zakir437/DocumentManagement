using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Services
{
    public class SendSMSModel
    {
        public string Message { get; set; }
        public string MobileNumber { get; set; }
    }
}
