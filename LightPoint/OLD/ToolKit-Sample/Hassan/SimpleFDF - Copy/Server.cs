using FactSet.Datafeed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SimpleFDF
{
    public class Server
    {
        private static Dictionary<RTSubscription, string> subscriptionCache = new Dictionary<RTSubscription, string>();
        private static DataSource source = DataSource.Instance();
        List<string> FuturesList = new List<string>();
        Stopwatch stopWatch = new Stopwatch();
        List<Dictionary<string, string>> valuePairs = new List<Dictionary<string, string>>();
        Dictionary<string, Dictionary<string, string>> futuresResult = new Dictionary<string, Dictionary<string, string>>();
        public Server()
        {
            Load();
        }

        private void Subscribe()
        {
            //StreamWriter writer = new StreamWriter(@"C:\Users\Hp\AppData\Local\FactSet\DataFeed\DotNetStandard\samples\SimpleFDF - Copy\Options2.txt");
            List<string> optionChain = new List<string>();
            Thread thread = new Thread(() =>
            {
               var IPaddresses = new List<string>()
                    {
                        "192.168.99.206",
                        "192.168.100.227"
                    };
                while (true)
                {
                    var tickers = source.Recieve();
                    RTRequest req = null;
                    foreach (var security in tickers)
                    { 
                        Console.WriteLine("Factset worker subscribe method called.");
                        //req = new RTRequest("FDS1", "AAPL#C2522C160000-USA" + ":D");
                        req = new RTRequest("FDS_FUT_CHAIN", "SI00-USA");
                        //req = new RTRequest("FDS1", "MSFT-US:D");
                        //req = new RTRequest("FDS_OPT_CHAIN", "AMD-US");
                        //req.Options = "months=4&strikes=60-60&call";
                        //req = new RTRequest("FDS_PERM", "LP_OMS-1301267");
                        //req.Options = "192.168.99.206,192.168.100.227";
                        //req.Options = "192.168.100.227";

                        stopWatch.Start();
                        RTSubscription sub = FDF.Request(req, delegate (RTSubscription rtSubscription, RTMessage rTMessage, RTRecord rtRecord) {
                            Console.WriteLine($"Message number {rtSubscription.Count} for {rtSubscription.Key}:");
                            Console.WriteLine($"\t{rTMessage.Key}");
                            //writer.WriteLine($"\t{rTMessage.Key}");
                            FuturesList.Add(rTMessage.Key);
                            

                            if (rTMessage.IsError == true)
                            {
                                Console.WriteLine($"Error Code: {rTMessage.Error} | Error: {rTMessage.ErrorDescription}");
                            }

                            foreach (RTFidField fld in rTMessage)
                            {
                                string fieldName = RTFieldMap.GetFieldName(fld.Id);
                                if ( fieldName.Contains("LAST_CHG") || fieldName.Contains("LAST_PCT_CHG"))
                                {
                                    Console.WriteLine("--------------------------" + fieldName + " Found" + "--------------------------");
                                }               
                                Console.WriteLine($"\t{fieldName}[{fld.Id}]: {fld.Value}");
                                //writer.WriteLine($"\t{fieldName}[{fld.Id}]: {fld.Value}");
                                

                            }
                            if (rTMessage.IsComplete)
                            {
                                var userName = "LP_OMS_1232158_SERVICES";
                                var password = "UgZ6tUdu6Y6zy8Tw";
                                var authToken = Encoding.ASCII.GetBytes($"{userName}:{password}");

                                using (var client = new HttpClient())
                                {

                                    client.DefaultRequestHeaders.Accept.Clear();
                                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                                    var url = "https://datadirect.factset.com/services/DataFetch";

                                    NameValueCollection queryStringParams = System.Web.HttpUtility.ParseQueryString(string.Empty);

                                    queryStringParams.Add("id", String.Join(",", FuturesList));
                                    queryStringParams.Add("format", "json");
                                    queryStringParams.Add("report", "SEC_REF_DD_LP_FUT");

                                    var requestURL = $"{url}?{queryStringParams.ToString()}";
                                    Console.WriteLine($"Request URL: {requestURL}");
                                    var requestHandler = client.GetAsync(requestURL).Result;

                                    if (requestHandler.IsSuccessStatusCode)
                                    {
                                        var result = requestHandler.Content.ReadAsStringAsync().Result;
                                        valuePairs = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result);
                                        stopWatch.Stop();
                                        TimeSpan ts = stopWatch.Elapsed;
                                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                        Console.WriteLine("Time taken: " + elapsedTime);
                                    }

                                }
                                foreach (var item in valuePairs)
                                {
                                    Dictionary<string, string> temp = new Dictionary<string, string>();
                                    foreach (var item2 in item)
                                    {
                                        
                                        if(item2.Key== "EXP_DATE")
                                        {
                                            temp.Add(item2.Key,item2.Value);
                                        }
                                        else if (item2.Key == "SEC_NAME")
                                        {
                                            temp.Add(item2.Key, item2.Value);
                                        }
                                        else if (item2.Key == "identifier")
                                        {
                                            futuresResult.Add(item2.Value,temp);
                                        }
                                    }
                                }
                                foreach (var item in futuresResult)
                                {
                                    Console.WriteLine($"\n\t{item.Key}");

                                    foreach (var item2 in item.Value)
                                    {
                                        Console.WriteLine($"\t[{item2.Key}]: {item2.Value}");

                                    }
                                }
                            }
                            //writer.Flush();
                            //writer.WriteLine("\n");
                            //if (!optionChain.Contains(rTMessage.Key))
                            //{
                            //    optionChain.Add(rTMessage.Key);
                            //}
                            //Console.WriteLine($"----------------Option Chain Count: {optionChain.Count}----------------");


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
