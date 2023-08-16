using Grpc.Net.Client;
using GrpcTest;
using System;

namespace GRPCClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client2 = new testProvider.testProviderClient(channel);

            var result = client2.GetResponse( new IRequest() { Name = "bilal", Num1 = 5, Num2 = 0 });

            Console.WriteLine(result.ToString());
        }
    }
}
