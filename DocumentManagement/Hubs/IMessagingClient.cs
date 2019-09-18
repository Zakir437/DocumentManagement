using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Hubs
{
    public interface IMessagingClient
    {
        Task Notification(string user, string message);
        //Task ReceiveMessage(string message);
    }
}
