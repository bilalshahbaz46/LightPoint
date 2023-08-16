using Grpc.Core;
using Grpc.Net.Client;
using GrpcClientCount;
using GrpcPopulation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcDemoClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var code = Console.ReadLine().ToString().ToLower();

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client1 = new PopulationProvider.PopulationProviderClient(channel);

            //var resultCount = client1.GetPopulation(new CountryRequest { State = code });
            //var resultName = client1.GetFullName(new CountryRequest { State = code });

            //Console.WriteLine(resultCount);
            //Console.WriteLine(resultName);

            //////////////////////////////////////////////////////////////////////////////////////////////////

            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client2 = new ClientCountProvider.ClientCountProviderClient(channel);
            try
            {
                //var result = client2.GetResponse(new IRequest() { Num1 = 5, Num2 = 0 });
                //Console.WriteLine(result.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.ToString()}");
            }



            //var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            //using var countResponse = client2.GetClientCount(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: tokenSource.Token);

            //try
            //{
            //    await foreach (var item in countResponse.ResponseStream.ReadAllAsync(tokenSource.Token))
            //    {
            //        Console.WriteLine(item);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("RPC Error : " + ex.Message);
            //}

        }
    }
}
