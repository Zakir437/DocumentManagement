using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using DocumentManagement.Services;

namespace DocumentManagement.Hubs
{
    public class MessagingHub : Hub
    {
        //private readonly IHubContext<MessagingHub> _hubContext;
        /*private readonly MessageContextUserIdProvider _userIdProvider;
        public MessagingHub(IHubContext<MessagingHub> hubContext)
        {
            _hubContext = hubContext;
        }*/
        //IHubContext context = Startup.ConnectionManager.GetHubContext<SomeHub>();
        public async Task SendNotification(int count)
        {
            await Clients.Others.SendAsync("GetNotification", count);
            //await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message); 
            //await Clients.User("spicyb.developer@gmail.com").SendAsync("GetNotification", count);
        }

        public async Task SendCurrentUserInfo()
        {
            var user = new {
                UserId = Context.User.GetUserId(),
                UserName = Context.User.GetUserName(),
                UserFullName = Context.User.GetUserFullName()
            };
            await Clients.User(Context.User.GetUserName()).SendAsync("GetCurrentUserInfo", user);
        }

        public async Task SendMessageToUser(MessagingViewModel model)
        {
            await Clients.User(model.SendToUserName).SendAsync("GetMessageFromUser", model);
        }
    }
}
