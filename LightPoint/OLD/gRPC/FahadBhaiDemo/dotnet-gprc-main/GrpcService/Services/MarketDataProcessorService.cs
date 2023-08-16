using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Bogus;
using Microsoft.Extensions.Logging;

namespace GrpcService.Services
{
    public interface MarketDataProcessorService {

        void process();
        void subscribe(String ticker, List<String> mnemoics);
        void unSubscribe(String ticker);

    }

    class SubscriptionItem {

        public String ticker { get; set; }
        public List<String> mnemeoics { get; set; }

    }


    public class MarketDataProcessorServiceImpl : MarketDataProcessorService
    {
        ConcurrentDictionary<String, List<String>> activeSubscriptions;

        private ILogger<MarketDataProcessorService> logger;

        private Faker faker;

        private BlockingCollection<SubscriptionItem> eventQueue;

        public MarketDataProcessorServiceImpl(ILogger<MarketDataProcessorService> logger)
        {
            Randomizer.Seed = new Random(8675309);
            this.activeSubscriptions = new ConcurrentDictionary<string, List<string>>();
            this.faker = new Faker();
            this.logger = logger;
            this.eventQueue = new BlockingCollection<SubscriptionItem>( new ConcurrentQueue<SubscriptionItem>() );
        }

        /**
         * 
         * 
         * 
         */
        public void subscribe(string ticker, List<string> mnemoics)
        {
            if (!this.activeSubscriptions.ContainsKey(ticker))
            {
                this.activeSubscriptions.TryAdd(ticker, mnemoics);

                logger.LogInformation($"Ticker {ticker} has been subscribed for updates");
            }
        }

        public void unSubscribe(string ticker)
        {
            if (this.activeSubscriptions.ContainsKey(ticker))
            {
                this.activeSubscriptions.TryRemove(ticker, out List<String> mnemoics);
            }
        }

        public void process()
        {
            this.ObserveSubscription();

            this.SubscriptionEmiiter();
        }

        /**
         * 
         * 
         */
        private void ObserveSubscription()
        {
            var t1 = new Thread(() => {
                while (true)
                {
                    if (!this.activeSubscriptions.IsEmpty)
                    {
                        List<KeyValuePair<string, List<string>>> subscriptionItemList = this.faker.PickRandom(this.activeSubscriptions, 1).ToList();
                        KeyValuePair<string, List<string>> subscriptionItem = subscriptionItemList[0];

                        logger.LogInformation($"Ticker Selected : {@subscriptionItem.Key}");

                        this.eventQueue.TryAdd( new SubscriptionItem() {
                            ticker = subscriptionItem.Key,
                            mnemeoics = subscriptionItem.Value
                        });
                    }
                    Thread.Sleep(100);
                }
            });
            t1.IsBackground = true;
            t1.Name = "ObserveSubscription";
            t1.Start();
        }

        private void SubscriptionEmiiter()
        {
            var t1 = new Thread(() =>
            {
                while (true)
                {
                    SubscriptionItem item = this.eventQueue.Take();

                    logger.LogInformation($"Emitting Changes for : {item.ticker}");
                }
            });
            t1.IsBackground = true;
            t1.Name = "SubscriptionEmiiter";
            t1.Start();
        }
    }
}
