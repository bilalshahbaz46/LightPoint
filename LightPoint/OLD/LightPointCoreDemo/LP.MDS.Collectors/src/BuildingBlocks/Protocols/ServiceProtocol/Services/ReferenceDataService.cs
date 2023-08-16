using CommandProtocol.Requestable;
using CommandProtocol.Transferable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProtocol.Services
{
    public class ReferenceDataService : IReferenceDataService
    {
        public Task<OutgoingMessage<ReferenceData>> Get(IncomingRequest<ReferenceDataRequest> request)
        {
            Task.FromResult(createResponse(request));
            return null;
        }


        private OutgoingMessage<ReferenceData> createResponse( IncomingRequest<ReferenceDataRequest> request)
        {
            return new OutgoingMessage<ReferenceData>()
            {
                correlationId = request.correlationId,
                datasource=request.datasource,
                requestor=request.requestor,
                timestamp=request.timestamp,
                userId=request.userId,
                version=request.version
            };
        }
    }
}
