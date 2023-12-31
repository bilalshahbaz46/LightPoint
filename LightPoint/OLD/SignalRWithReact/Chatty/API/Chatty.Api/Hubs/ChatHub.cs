﻿using Chatty.Api.Hubs.Clients;
using Chatty.Api.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chatty.Api.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(ChatMessage message)
        {
            //await Clients.All.SendAsync("RecieveMessage", message);
            await Clients.All.ReceiveMessage(message);
        }
    }
}
