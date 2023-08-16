using Grpc.Core;
using GrpcTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRPCServer
{
    public class testService : testProvider.testProviderBase
    {
        public override Task<OutgoingMessage> GetResponse(IRequest request, ServerCallContext context)
        {
            OutgoingMessage message = new OutgoingMessage();
            try
            {
                var div = request.Num1 / request.Num2;
                message.Success = new SuccessMessage() { Ans = "The request successfully went through!" };
            }
            catch (Exception e)
            {
                message.Error = new ErrorMessage() { ErrorMessage_ = e.ToString() };
            }

            return Task.FromResult(message);
        }
    }
}
