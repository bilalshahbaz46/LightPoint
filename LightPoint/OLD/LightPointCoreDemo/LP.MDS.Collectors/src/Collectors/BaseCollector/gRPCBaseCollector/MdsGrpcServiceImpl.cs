using CommandProtocol.Requestable;
using Grpc.Core;
using GrpcService.MDS;
using Microsoft.Extensions.Logging;
using ServiceProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReferenceDataRequest = GrpcService.MDS.ReferenceDataRequest;

namespace gRPCBaseCollector
{
    public class MdsGrpcServiceImpl: MdsGrpcService.MdsGrpcServiceBase
    {
        private IMarketDataService _marketDataService;
        private IReferenceDataService _referenceDataService;
        private readonly ILogger<MdsGrpcServiceImpl> _logger;
        public MdsGrpcServiceImpl(IReferenceDataService referenceDataService, ILogger<MdsGrpcServiceImpl> logger)
        {
            //_marketDataService = marketDataService;
            _referenceDataService = referenceDataService;
            _logger = logger;
        }
        public override Task<GrpcService.MDS.ReferenceData> GetReferenceData(ReferenceDataRequest request, ServerCallContext context)
        {
            var result= _referenceDataService.Get(GetReferenceData());
            ReferenceData data = new ReferenceData();
            data.Items.Add(new DataItem() {
            Message="Hello",
            Ticker="USA"
            });
            _logger.LogInformation("log - \'GetReferenceData\' gRPC Function");
            return Task.FromResult(data);
        }

        private IncomingRequest<CommandProtocol.Requestable.ReferenceDataRequest> GetReferenceData()
        {
            SecurityDefinition security = new SecurityDefinition
            {
                identifierType = "01",
                lastUpdate = "",
                message = "",
                securityIdentifier = "A"
            };

            return new IncomingRequest<CommandProtocol.Requestable.ReferenceDataRequest>() {
                correlationId = "",
                datasource = "",
                requestor = "",
                timestamp = "",
                userId = "",
                version = "",
                values = new CommandProtocol.Requestable.ReferenceDataRequest() {
                    securities = new List<SecurityDefinition>() { security }
                },

            };
        }

        private IncomingRequest<CommandProtocol.Requestable.SubscriptionRequest> GetSubscription()
        {
            SecurityDefinition security = new SecurityDefinition
            {
                identifierType = "01",
                lastUpdate = "",
                message = "",
                securityIdentifier = "A"
            };

            return new IncomingRequest<CommandProtocol.Requestable.SubscriptionRequest>()
            {
                correlationId = "",
                datasource = "",
                requestor = "",
                timestamp = "",
                userId = "",
                version = "",
                values = new CommandProtocol.Requestable.SubscriptionRequest()
                {
                    securities = new List<SecurityDefinition>() { security }
                },

            };
        }
    }
}
