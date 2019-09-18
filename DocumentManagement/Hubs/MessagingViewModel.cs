using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Hubs
{
    public class MessagingViewModel
    {
        public string SendFromUserName { get; set; }
        public string SendToUserName { get; set; }
        public string Message { get; set; }
        public DateTime? SendDateTime { get; set; }
    }
}
