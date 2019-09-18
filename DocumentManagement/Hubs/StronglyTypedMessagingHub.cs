using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Hubs
{
    public class StronglyTypedMessagingHub : Hub<IMessagingClient>
    {
        public async Task SendOthers(string user, string message)
        {
            await Clients.Others.Notification(user, message);
        }
    }
}
