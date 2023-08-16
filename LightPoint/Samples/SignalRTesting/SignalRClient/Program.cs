using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace SignalRClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter once the server is up");
            Console.ReadLine();
            var service = new HubServices();
            service.sendMessage("Hello Bilal this side.");
        }

    }
}
