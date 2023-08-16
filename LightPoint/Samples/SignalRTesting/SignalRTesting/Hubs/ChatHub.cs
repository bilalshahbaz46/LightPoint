using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTesting.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection Established " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceivedConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task Send(string msg)
        {
            Console.WriteLine("recieved a request from client");
            await Clients.All.SendAsync("Recieved Message", msg);
        }
    }
}