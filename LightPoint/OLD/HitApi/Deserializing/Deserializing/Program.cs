using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Deserializing
{

    public class SearchAPIRequestModel
    {
        public Query query { get; set; }
        public Settings settings { get; set; }
    }

    public class Query
    {
        public string pattern { get; set; }
        public string[] entities { get; set; }
    }

    public class Settings
    {
        public int result_limit { get; set; }
    }


    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            //var letterList = Enumerable.Range('A', 'Z' - 'A' + 1).
            //             Select(c => (char)c).ToList();

            //string[] prefixes = new string[] { "AA", "AB", "AC", "BA", "BB", "BC", "CA", "CB", "CC", "DA", "DB", "DC",
            //    "EA", "EB", "EC", "FA", "FB", "FC", "GA", "GB", "GC", "HA", "HB", "HC", "IA", "IB", "IC", "JA", "JB", "JC",
            //"KA", "KB", "KC", "LA", "LB", "LC", "MA", "MB", "MC", "NA", "NB", "NC", "OA", "OB", "OC", "PA", "PB", "PC", "QA", 
            //"QB", "QC", "RA", "RB", "RC", "SA", "SB", "SC", "TA", "TB", "TC"};
            //var pCount = prefixes.Count();

            //var list = new List<Root>();
            //var interval = 0;
            //foreach (var _p in prefixes)
            //{
            //    Console.WriteLine($"{interval}/{pCount}");
            //    foreach (var alpha in letterList)
            //    {
            //        list.Add(await ReferenceData($"{_p}{alpha}"));
            //    }
            //    interval++;
            //}



            //var lines = new List<string>();
            //foreach (var item in list)
            //{
            //    foreach (var i in item.typeahead.results)
            //    {
            //        lines.Add(i.ticker);
            //    }
            //}

            //await File.WriteAllLinesAsync(@"D:\Work\LightPoint\HitApi\Deserializing\Deserializing\Tickers.txt", lines);

            //Console.WriteLine("DONE!");


            string[] lines = File.ReadAllLines(@"D:\Work\LightPoint\HitApi\Deserializing\Deserializing\Tickers.txt");

            var list = new List<string>();
            foreach (string line in lines)
                list.Add(line);

            var Distinctlist = list.Distinct();

            await File.WriteAllLinesAsync(@"D:\Work\LightPoint\HitApi\Deserializing\Deserializing\DistinctTickers.txt", Distinctlist);

            Console.ReadLine();
        }

        private static async Task<Root> ReferenceData(String ticker)
        {
            var userName = "LP_OMS-1232158";
            var password = "5k1zbYwPE92GWP8UayoVTMaTAQNzQBggyGv1SJq9";
            var authToken = Encoding.ASCII.GetBytes($"{userName}:{password}");

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                //Get Request
                //var result = await client.GetAsync($"https://datadirect.factset.com/services/DataFetch?report=SEC_REF_DD_LP&id={ticker}&format=xml");

                var query = new Query()
                {
                    pattern = ticker,
                    entities = new string[]{ "equities", "bonds", "futures" }
                };
                var settings = new Settings()
                {
                    result_limit = 100
                };

                var RequestObject = new SearchAPIRequestModel()
                {
                    query = query,
                    settings = settings
                };

                var JsonObject = JsonConvert.SerializeObject(RequestObject);
                var stringContent = new StringContent(JsonObject.ToString());

                var result = await client.PostAsync($"https://api.factset.com/idsearch", stringContent);
                //XmlDocument doc = new XmlDocument();
                //doc.Load(await result.Content.ReadAsStreamAsync());

                var doc = result.Content.ReadAsStringAsync().Result;

                var response = JsonConvert.DeserializeObject<Root>(doc);


                return response;
            }
        }



        //static async Task CallWebAPIAsync()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        //Send HTTP request from here.


        //        //FAHAD BHAI
        //        DataItem item = await ReferenceData("IBM");
        //        Dictionary<String, String> nenomincs = new Dictionary<string, string>();
        //        nenomincs.Add("COUNTRY_NAMNE", "USA");

        //        //client.BaseAddress = new Uri("https://datadirect.factset.com/services");



        //        //Get Request
        //        var result = await client.GetAsync("https://datadirect.factset.com/services/DataFetch?report=SEC_REF_DD_LP&id=appl&format=xml");
        //        XmlDocument doc = new XmlDocument();
        //        doc.Load(await result.Content.ReadAsStreamAsync());

        //        Console.WriteLine(doc.InnerXml);
        //        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        //        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        //        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        //        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        //        Console.WriteLine("-----------------------------------------------------------------------------------------------------");

        //        var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, false);

        //        Console.WriteLine(json);


        //        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        //        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        //        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        //        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        //        Console.WriteLine("-----------------------------------------------------------------------------------------------------");

        //        //var Items = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(json);
        //        var Items = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

        //        Console.WriteLine(Items);

        //    }
        //}
    }
}
