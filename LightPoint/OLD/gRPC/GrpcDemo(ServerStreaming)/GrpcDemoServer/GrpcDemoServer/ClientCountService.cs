using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace GrpcDemoServer
{
    //public class ClientCountService : TestProvider
    //{
    //    public override async Task GetClientCount(Empty request, IServerStreamWriter<ClientCountResponse> responseStream, ServerCallContext context)
    //    {
    //        var count = 0;
    //        while (!context.CancellationToken.IsCancellationRequested)
    //        {
    //            await responseStream.WriteAsync(new ClientCountResponse { Count = count });
    //            count++;
    //            await Task.Delay(50);
    //        }
    //    }

    //    public override Task<IResponse> GetResponse(OutgoingMessage request, ServerCallContext context)
    //    {
    //        OutgoingMessage message = new OutgoingMessage();
    //        try
    //        {
    //            message.Success = new SuccessMessage() { Ans = "The request successfully went through!" };
    //        }
    //        catch (Exception e)
    //        {
    //            message.Error = new ErrorMessage() { ErrorMessage_ = e.ToString() };
    //        }

    //        return Task.FromResult(message);
    //    }

    //    public override Task<IResponse> GetResponse(OutgoingMessage request, ServerCallContext context)
    //    {
    //        return base.GetResponse(request, context);
    //    }
    //}

    public class ClientCountService : TestProvider.TestProviderBase
}
