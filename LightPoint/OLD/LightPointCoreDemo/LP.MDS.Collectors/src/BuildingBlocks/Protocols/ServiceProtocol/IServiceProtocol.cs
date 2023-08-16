using CommandProtocol.Requestable;
using CommandProtocol.Transferable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProtocol
{
    public interface IServiceProtocol
    {
    }

    public interface IMarketDataService
    {
        OutgoingMessage<ReferenceData> Subscribe(SubscriptionRequest request);
        bool Unsubscribe(SubscriptionRequest request);
        bool hasSubscription(SubscriptionRequest request);

    }


    public interface IReferenceDataService
    {
        Task<OutgoingMessage<ReferenceData>> Get(IncomingRequest<ReferenceDataRequest> request);
    }

    
}
