using FactSet.Datafeed;
using SimpleFDF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Simple {
    class Program {

        private static bool keepRunning = true;
        private static void PrintAllFields(RTSubscription sub, RTMessage msg, RTRecord rec) {
            if (msg.IsError) {
                Console.WriteLine($"Error for {msg.Key}: {msg.ErrorDescription}");
                keepRunning = false;
                return;
            } else if (msg.IsClosed) {
                Console.WriteLine($"Message for {msg.Key} is marked as closed");
                keepRunning = false;
                return;
            }

            Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} | Message number {sub.Count} for {sub.Key}:");
            foreach (RTFidField fld in msg) {
                string fieldName = RTFieldMap.GetFieldName(fld.Id);
                Console.WriteLine($"\t{fieldName}[{fld.Id}]: {fld.Value}");
            }
        }


        static void Main(string[] args)
        {
            #region Old Code
            string[] tickers = File.ReadAllLines(Environment.CurrentDirectory + @"\tickers.txt");
            Server server = new Server();
            server.StartAsync();

            Client[] clients = new Client[1];

            Parallel.For(0, 1, index =>
            {
                clients[index] = new Client(tickers);
                clients[index].Request();
            });

            //commented code//////////////////////////////////////////////////////////////
            //Client client1 = new Client(tickers);
            //client1.Request();

            //Client client2 = new Client(tickers);
            //client2.Request();

            //Client client3 = new Client(tickers);
            //client3.Request();

            //Client client4 = new Client(tickers);
            //client4.Request();
            #endregion


            #region User Permissions

            //RTConsumer consumer = new RTConsumer();
            //FDF.ConnInfo = "LP_OMS-1231139:IaOh5VQMvLGx0SPX@api-stage.df.factset.com";
            //FDF.Connect();

            //Console.WriteLine($"Connected to server");
            //RTRequest permreq = new RTRequest("FDS_PERM", "LP_OMS-1301267");
            ////permreq.Options = "103.225.222.222";
            //RTSubscription subs = consumer.Request(permreq, (RTSubscription sub, RTMessage msg, RTRecord rtRecord) =>
            //{
            //    if (msg.ContainsField(RTFieldId.USER_PERMISSIONS))
            //    {
            //        // USER_PERMISSIONS is a repeated field, it will repeat in the message and needs to be iterated to get the 
            //        //complete list
            //        var i = msg.GetEnumerator();
            //        string permission = "";
            //        foreach (var p in msg)
            //        {
            //            permission = permission + "," + p.Value;
            //        }
            //        Console.WriteLine($"Perm field : {permission}");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Message : {msg}");
            //    }
            //});
            //Console.WriteLine("Starting to dispatch");
            //while (true)
            //{
            //    consumer.Dispatch(1000);
            //}
            #endregion


        }
    }
}
