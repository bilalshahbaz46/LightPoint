using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GprcService;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService.MDS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GprcClient
{
    class Program
    {
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddSerilog());

            //we will configure logging here
        }

        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("serilog.json")
            .Build();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            //var logger = serviceProvider.GetService<ILogger<Program>>();

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var mnemoics = new List<String>();
            mnemoics.Add( "PX_LAST" );
            mnemoics.Add( "ASK_PRICE" );
            mnemoics.Add( "BID_PRICE" );
            mnemoics.Add( "SECURITY_TYPE" );
            mnemoics.Add( "SHORT_NAME" );

            var channel = GrpcChannel.ForAddress("http://localhost:5000");

            var greeterClient = new Greeter.GreeterClient( channel );

            var helloRequest = new HelloRequest();
            helloRequest.Name = "[Fahad from Gprc Client]";

            var response = greeterClient.SayHello(helloRequest);


            var mdsConnector = new MdsGrpcService.MdsGrpcServiceClient(channel);
            var request = new ReferenceDataRequest();
            request.Tickers.Add("IBM US EQUITY");
            request.Tickers.Add("AMGN US EQUITY");
            request.Tickers.Add("APPL US EQUITY");

            request.Mnemonics.AddRange(mnemoics.ToList());
            var referenceDataResult = mdsConnector.GetReferenceData(request);
            logger.Information($"Reference Data REsponse {@referenceDataResult}");



            var subscriptionRequest = new SubscriptionRequest();
            subscriptionRequest.Tickers.Add("IBM US EQUITY");
            subscriptionRequest.Tickers.Add("AMGN US EQUITY");
            subscriptionRequest.Tickers.Add("APPL US EQUITY");


            var subscriptionResult = mdsConnector.Subscribe(subscriptionRequest);

            logger.Information($"Subscription Request:: {@subscriptionRequest}");



            subscriptionRequest = new SubscriptionRequest();
            subscriptionRequest.Tickers.Add("HOME US EQUITY");
            subscriptionRequest.Tickers.Add("AMD US EQUITY");
            subscriptionRequest.Tickers.Add("FB US EQUITY");
            subscriptionRequest.Tickers.Add("NFLX US EQUITY");
            subscriptionRequest.Tickers.Add("GOOGL US EQUITY");
            subscriptionRequest.Tickers.Add("MSFT US EQUITY");
            subscriptionRequest.Tickers.Add("TSLA US EQUITY");
            subscriptionRequest.Tickers.Add("AMZN US EQUITY");


            subscriptionResult = mdsConnector.Subscribe(subscriptionRequest);
            logger.Information($"Subscription Request:: {@subscriptionRequest}");


            using var subsciptionHandler = mdsConnector.MarketDataHandler(new Google.Protobuf.WellKnownTypes.Empty());
            while (await subsciptionHandler.ResponseStream.MoveNext())
            {
                DataItem streamUpdate = subsciptionHandler.ResponseStream.Current as DataItem;

                logger.Information($"Updated Ticket is : {streamUpdate.Ticker}");
            }

            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
        }
    }
}
