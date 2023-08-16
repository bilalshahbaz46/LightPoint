using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRClient
{
    public class HubServices
    {
        private readonly HubConnection hubConnection;
        public HubServices()
        {
            this.hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5000/chathub")
                .Build();
        }

         public void sendMessage(string msg)
        {
            this.hubConnection.StartAsync();
            Console.WriteLine("Sending the message from Client...");
            hubConnection.SendAsync("OnConnectedAsync");
            hubConnection.SendAsync("SendMessageAsync", msg);
            Console.WriteLine("Sent the message from Client!");

        }
    }


}
