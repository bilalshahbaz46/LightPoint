using Grpc.Net.Client;
using GrpcPopulation;
using GrpcTest;
using System;

namespace GrpcDemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var code = Console.ReadLine().ToString().ToLower();

            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new PopulationProvider.PopulationProviderClient(channel);

            //var resultCount = client.GetPopulation(new CountryRequest { State = code});
            //var resultName = client.GetFullName(new CountryRequest { State = code});

            //Console.WriteLine(resultCount);
            //Console.WriteLine(resultName);


            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new testProvider.testProviderClient(channel);

            var input = Console.ReadLine();

            var result = client.GetResponse(new IRequest() {  Name = input});

            Console.WriteLine($"Result:{result.ToString()}");
        }
    }
}
