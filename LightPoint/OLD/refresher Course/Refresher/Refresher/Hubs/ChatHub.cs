using Grpc.Net.Client;
using GrpcUserPoints;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Refresher.Dtos.Users;
using Refresher.GrpcServices.Users;
using Refresher.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Refresher.Hubs
{
    public class ChatHub : Hub
    {
        private IUserService _service;
        private GrpcUserService _grpcUserServices;
        public ChatHub(IUserService service, GrpcUserService grpcUserService)
        {
            _service = service;
            _grpcUserServices = grpcUserService;
        }
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection Established " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceivedConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task SendMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
            string toClient = routeOb.To;
            Console.WriteLine("Message Received on ConnectionID: " + Context.ConnectionId);

            if (toClient == string.Empty)
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            else
            {
                await Clients.Client(toClient).SendAsync("ReceiveMessage", message);
            }
        }

        //Method for the HTML client
        public async Task AddNewUserHTML(string request)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(request);
            _service.AddUser(new CreateUserDto {UserName=routeOb.UserName, Points=routeOb.UserPoint});
        }

        //Method for the C# Console App Client
        public async Task AddNewUserConsole(string username, double userPoints)
        {
            _service.AddUser(new CreateUserDto { UserName = username, Points = userPoints });
        }

        public async Task GetAllUsersConsole()
        {
            var result = _service.GetUsers();
            await Clients.Client(Context.ConnectionId).SendAsync("ReceivedUsers", result);
        }

    }
}
