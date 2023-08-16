using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            await CallPingReply(new Ping.PingClient(channel));
        }

        private static async Task CallPingReply(Ping.PingClient client)
        {
            var request = new Message { Msg = "Hello Ping!" };
            var cancellationToken = new CancellationTokenSource(
              TimeSpan.FromSeconds(60));

            try
            {
                AsyncServerStreamingCall<Message> response =
                    client.DoRepeatReply(request,
                      cancellationToken: cancellationToken.Token);

                while (await response.ResponseStream.MoveNext())
                {
                    var current = response.ResponseStream.Current;
                    
                    Console.WriteLine($"{current.Msg}");
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Operation Cancelled.");
            }

            Console.ReadLine();
        }
    }
}
