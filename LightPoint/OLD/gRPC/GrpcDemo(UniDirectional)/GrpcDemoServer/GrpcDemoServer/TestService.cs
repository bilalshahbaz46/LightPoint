using Grpc.Core;
using GrpcTest;
using System;
using System.Threading.Tasks;

namespace GrpcDemoServer
{
    public class TestService : testProvider.testProviderBase
    {
        public override Task<OutgoingMessage> GetResponse(IRequest request, ServerCallContext context)
        {
            OutgoingMessage message = new OutgoingMessage();
            SuccessMessage success = new SuccessMessage();
            ErrorMessage error = new ErrorMessage();
            try
            {
                if (request.Name == "request" || request.Name == "REQUEST")
                {
                    success.Ans = "The request went through";
                    message.Success = success;
                }
                else
                    throw new Exception("Try the Names \"request\" or \"REQUEST\"");
            }
            catch(Exception e)
            {
                error.ErrorMessage_ = e.Message.ToString();
                message.Error = error;
            }

            return Task.FromResult(message);
        }
    }
}
