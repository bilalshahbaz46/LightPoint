using System;
using System.Collections.Generic;
using GrpcService.MDS;

namespace GrpcService.Proxy
{
    public class CollectorProxy
    {
        public ReferenceData process(List<String> tickers, List<String> mnemonics)
        {
            ReferenceData referenceData = new ReferenceData();
            foreach(String ticker in tickers)
            {
                referenceData.Items.Add( this.process(ticker, mnemonics));
            }

            return referenceData;
        }

        private DataItem process(String ticker, List<String> mnemonics)
        {

            return new DataItem()
            {
                Ticker = ticker + "[Server Side]",
            };
        }
    }
}
