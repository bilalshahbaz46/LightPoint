using Grpc.Core;
using Grpc.Net.Client;
using GrpcTicker;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssignmentStartOrdering
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new TickerProvider.TickerProviderClient(channel);

            var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var result = client.RunTicking(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: tokenSource.Token);

            try
            {
                await foreach(var item in result.ResponseStream.ReadAllAsync(tokenSource.Token))
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"The exception returned: {e.Message}");
            }
        }
    }
}
