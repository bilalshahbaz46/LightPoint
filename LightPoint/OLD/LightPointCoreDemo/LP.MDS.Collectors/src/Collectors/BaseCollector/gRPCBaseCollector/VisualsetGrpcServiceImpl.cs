using Google.Protobuf.Collections;
using Grpc.Core;
using GrpcService.Visualset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPCBaseCollector
{
    public class VisualsetGrpcServiceImpl : VisualsetGrpcService.VisualsetGrpcServiceBase
    {


        public override Task<ReferenceData> GetReferenceData(ReferenceDataRequest request, ServerCallContext context)
        {
            
        }

        private ReferenceData AddTickerData(string ticker)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Name", $"{ticker}-Name");
            dic.Add("Value", $"{ticker}-Value");
            dic.Add("Name", $"{ticker}-Message");

            MapField<string, string> mnemonics = new MapField<string, string>();
            mnemonics.Add(dic);


            DataItem item = new DataItem
            {
                Ticker = ticker,
                Mnemonics = mnemonics,
            };
        }
    }
}
