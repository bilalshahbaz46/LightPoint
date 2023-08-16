using Grpc.Core;
using Grpc.Net.Client;
using GrpcTicker;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment1Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tick = int.Parse(Console.ReadLine());

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new TickerProvider.TickerProviderClient(channel);
            var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(60));

            using var result = client.RunProgram(new TickerRequest { StoreTicker = tick }, cancellationToken: tokenSource.Token);

            try
            {
                await foreach (var item in result.ResponseStream.ReadAllAsync(tokenSource.Token))
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("RPC Error : " + ex.Message);
            }

        }
        }
    }
