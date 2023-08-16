using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GrpcService.MDS;

namespace GrpcService.Services
{
    public interface SubscriptionService {

        MarketData UnSubscribe(UnSubscriptionRequest request);

        MarketData Subscribe(SubscriptionRequest request);

    }

    public class SubscriptionServiceImpl : SubscriptionService
    {
        private readonly MarketDataProcessorService processorService;

        public SubscriptionServiceImpl(MarketDataProcessorService processorService)
        {
            this.processorService = processorService;
        }

        public MarketData UnSubscribe(UnSubscriptionRequest request)
        {
            MarketData response = new MarketData();
            List<String> tickers = new List<string>();
            foreach (String ticker in request.Tickers)
            {
                processorService.unSubscribe(ticker);
                response.Items.Add(new DataItem()
                {
                    Ticker = ticker,
                    Message = "Ticket has been unsubscribed"
                });
            }
            return response;
        }


        public MarketData Subscribe(SubscriptionRequest request)
        {
            MarketData response = new MarketData();
            foreach(String ticker in request.Tickers)
            {
                processorService.subscribe(ticker, request.Mnemonics.ToList<String>() );
                response.Items.Add(new DataItem()
                {
                    Ticker = ticker,
                    Message = "Ticket has been subscribed"
                });
            }
            return response;
        }

    }
}
