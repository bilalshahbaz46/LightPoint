using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcTicker;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment1.GrpcServices
{
    public class GrpcTickerService : TickerProvider.TickerProviderBase
    {
        private int[] Stores = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private List<int> Orders = new List<int>();


        Random rnd = new Random();

        public override async Task RunProgram(TickerRequest request, IServerStreamWriter<TickerResponse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                if (Orders.Contains(request.StoreTicker))
                {
                    await responseStream.WriteAsync(new TickerResponse { TickerStore = request.StoreTicker });
                }

                await responseStream.WriteAsync(new TickerResponse { TickerStore = 69 });
                await Task.Delay(3000);
            }
        }


        public override async Task RunTicking(Empty request, IServerStreamWriter<TickingResponse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                int i = 1;
                while (i <= 10)
                {
                    var num = rnd.Next(10);
                    Orders.Add(num);
                    i++;
                }

                if (Orders.Contains(4))
                {
                    await responseStream.WriteAsync(new TickingResponse { Changed = "Just Changed", Number = 4 });
                }
                else
                {
                    await responseStream.WriteAsync(new TickingResponse { Changed = "Just Changed", Number = 1 });
                }

                await Task.Delay(3000);
                Orders.Clear();
            }
        }




        //public async Task CreateNewOrders()
        //{
        //    while (true)
        //    {
        //        int i = 1;
        //        while (i <= 10)
        //        {
        //            var num = rnd.Next(10);
        //            Orders.Add(num);
        //            i++;
        //        }

        //        await Task.Delay(3000);
        //        Orders.Clear();
        //    }
        //}

    }
}
