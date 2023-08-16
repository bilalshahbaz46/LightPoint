using Grpc.Core;
using Grpc.Net.Client;
using GrpcService.MDS;
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

            var client2 = new MdsGrpcService.MdsGrpcServiceClient(channel);

            var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(60));

            var request = new ReferenceDataRequest();
            request.Tickers.Add("T1");
            request.Tickers.Add("T2");


            var response = client2.GetReferenceData(request);

        }
    }
}
