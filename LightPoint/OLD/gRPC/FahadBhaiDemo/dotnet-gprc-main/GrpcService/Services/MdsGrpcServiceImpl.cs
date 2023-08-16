using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.MDS;
using GrpcService.Proxy;
using Microsoft.Extensions.Logging;

namespace GrpcService.Services
{
    public class MdsGrpcServiceImpl : MDS.MdsGrpcService.MdsGrpcServiceBase
    {

        private readonly ILogger<MdsGrpcServiceImpl> logger;
        private readonly CollectorProxy proxy;
        private readonly SubscriptionService subscriptionService;

        public MdsGrpcServiceImpl(ILogger<MdsGrpcServiceImpl> logger, SubscriptionService subscriptionService)
        {
            this.logger = logger;
            proxy = new CollectorProxy();
            this.subscriptionService = subscriptionService;
        }

        /**
         * 
         * 
         * 
         * 
         */
        public override Task<ReferenceData> GetReferenceData(ReferenceDataRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            logger.LogInformation($"Connection id: {httpContext.Connection.Id}");

            return Task.FromResult( proxy.process( request.Tickers.ToList(), request.Mnemonics.ToList() ));
        }

        public override async Task MarketDataHandler(Empty request, IServerStreamWriter<DataItem> responseStream, ServerCallContext context)
        {
            logger.LogInformation($"Connection id: {context.GetHttpContext().Connection.Id}");

            while (true)
            {

                await responseStream.WriteAsync(
                    new DataItem()
                    {
                        Ticker = $"From Server {DateTime.Now.ToShortTimeString()}"
                    }
                ); 

                await Task.Delay(500);
            }
        }

        /**
         * 
         * 
         * 
         */
        public override Task<MarketData> Subscribe(SubscriptionRequest request, ServerCallContext context)
        {
            var subscriptionResult = subscriptionService.Subscribe(request);
            return Task.FromResult(  subscriptionResult );
        }

        public override Task<MarketData> UnSubscribe(UnSubscriptionRequest request, ServerCallContext context)
        {
            MarketData removedTickers = subscriptionService.UnSubscribe(request);
            return Task.FromResult(removedTickers);
        }
    }
}
