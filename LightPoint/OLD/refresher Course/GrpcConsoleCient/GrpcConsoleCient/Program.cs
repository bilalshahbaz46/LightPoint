using Grpc.Net.Client;
using GrpcUserPoints;
using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcConsoleCient.Models;

namespace GrpcConsoleCient
{
    class Program
    {
        static void Main(string[] args)
        {
            ////GRPC Client
            ////////////////////////////////////////////////////////////////
            //var str = Console.ReadLine();
            //var userId = long.Parse(str);
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new UserPointsProvider.UserPointsProviderClient(channel);

            //var points = client.GetUserPoints(new UserPointsRequest { UserId = userId });
            //Console.WriteLine($"The Points for the user with UserId \"{userId}\": {points}");



            //SignalR Client
            ////////////////////////////////////////////////////////////////

            var hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/chathub")
                .Build();

            hubConnection.StartAsync();

            hubConnection.On("ReceivedConnID", (string connId) =>
            {
                Console.WriteLine($"The Connection is established and the connection Id is: {connId} \n");
            });

            //Console.WriteLine("Enter the new username: ");
            //var username = Console.ReadLine();

            //Console.WriteLine("Enter the new user's point: ");
            //var userPoints = double.Parse(Console.ReadLine());

            //hubConnection.InvokeAsync("AddNewUserConsole", username, userPoints);
            hubConnection.InvokeAsync("GetAllUsersConsole");
            hubConnection.On("ReceivedUsers", (List<UserDto> e) =>
             {
                 var i = 1;
                 foreach (var item in e)
                 {
                     Console.WriteLine($"{i}.\tUserName: " + item.UserName);
                     Console.WriteLine("\tPoints: " + item.Points);
                     i++;
                 }
             });

            Console.ReadLine();

        }
    }
}
