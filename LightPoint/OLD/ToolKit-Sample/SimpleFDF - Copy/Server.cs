using FactSet.Datafeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFDF
{
    public class Server
    {
        private static Dictionary<RTSubscription, string> subscriptionCache = new Dictionary<RTSubscription, string>();
        private static DataSource source = DataSource.Instance();

        public Server()
        {
            Load();
        }

        private void Subscribe()
        {
            List<string> optionChain = new List<string>();
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    var tickers = source.Recieve();
                    RTRequest req = null;
                    foreach (var security in tickers)
                    {
                        //Console.WriteLine("Factset worker subscribe method called.");
                        //req = new RTRequest("FDS1", "AAPL#R1722D525000-USA" + ":D");
                        //req = new RTRequest("FDS_PERM", "LP_OMS-1301267");
                        //req.Options = "192.168.99.206,192.168.100.227";
                        //req.Options = "192.168.99.206";
                        //req.Options = "192.168.100.227";
                        //req = new RTRequest("FDS_OPT_CHAIN", "AMD-US");
                        //req.Options = "months=4&strikes=60-60&call";

                        RTSubscription sub = FDF.Request(req, delegate (RTSubscription rtSubscription, RTMessage rTMessage, RTRecord rtRecord) {
                            Console.WriteLine($"Message number {rtSubscription.Count} for {rtSubscription.Key}:");
                            Console.WriteLine($"\t{rTMessage.Key}");


                            foreach (RTFidField fld in rTMessage)
                            {
                                string fieldName = RTFieldMap.GetFieldName(fld.Id);
                                Console.WriteLine($"\t{fieldName}[{fld.Id}]: {fld.Value}");
                            }

                            //var field = rTMessage.FirstOrDefault(x => RTFieldMap.GetFieldName(x.Id) == "USER_LOGIN_STATUS").Value;

                            //var i = field;


                            if (!optionChain.Contains(rTMessage.Key))
                            {
                                optionChain.Add(rTMessage.Key);
                            }

                            if(rTMessage.IsError == true)
                            {
                                var e = rTMessage.Error;
                            }
                            Console.WriteLine($"----------------Option Chain Count: {optionChain.Count}----------------");


                            var incomingrequest = subscriptionCache.GetValueOrDefault(rtSubscription); 

                        });
                        subscriptionCache.Add(sub, security);
                    }
                }
            });
            thread.Start();
            
        }

        

        public Task StartAsync()
        {
            this.ProcessHandler();

            this.Subscribe();

            return Task.CompletedTask;

        }

        public void Load()
        {
            try
            {
                RTFieldMap.Create("../../../../etc/rt_fields.xml");
            }
            catch (DatafeedException e)
            {
                Console.WriteLine($"Error while creating fieldmap, was the filename and path correct? Error message:\n\t{e.Message}");
                return;
            }
            FDF.ConnInfo = "LP_OMS-1231139:IaOh5VQMvLGx0SPX@api-stage.df.factset.com";
            FDF.Connect();
        }

        private void ProcessHandler()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    FDF.Dispatch(1000);
                }
            });
        }
    }
}
