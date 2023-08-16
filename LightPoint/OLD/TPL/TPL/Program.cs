using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace TPL
{
    class Program
    {
        static void Main(string[] args)
        {
            //    Console.WriteLine("Hello World");

            //    var _producer = producer();

            //    var _consumer1 = Program.consumer("Consumer 1");
            //    var _consumer2 = Program.consumer("Consumer 2");

            //    _producer.LinkTo(_consumer1, new DataflowLinkOptions { PropagateCompletion = true } );
            //    _producer.LinkTo(_consumer2, new DataflowLinkOptions { PropagateCompletion = true });

            //    var sw = new Stopwatch();
            //    sw.Start();
            //    for(int i=0; i<10; i++)
            //    {
            //        if (_producer.Post(i))
            //        {
            //            Console.WriteLine("Success");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Failed");
            //        }
            //    }

            //    _producer.Complete();

            //     //Consumer 1
            //    _consumer1.Completion.ContinueWith(p =>
            //    {
            //        sw.Stop();
            //        Console.WriteLine("Consumer 1 done - " + sw.ElapsedMilliseconds);
            //    });

            //    //Consumer 2
            //    _consumer2.Completion.ContinueWith(p =>
            //    {
            //        sw.Stop();
            //        Console.WriteLine("Consumer 2 done - " + sw.ElapsedMilliseconds);
            //    });

            //    Console.ReadLine();
            //}

            //static ActionBlock<int> consumer(string name)
            //{
            //    var block = new ActionBlock<int>((timeout) => 
            //    {
            //        Thread.Sleep(timeout);
            //        Console.WriteLine($"In the action block {name} - {timeout}");
            //    }, new ExecutionDataflowBlockOptions 
            //    {
            //        MaxDegreeOfParallelism = 2,
            //        BoundedCapacity = 2
            //    });

            //    return block;
            //}

            //static TransformBlock<int, int> producer()
            //{
            //    var block = new TransformBlock<int, int>(input => input*2);
            //    return block;
            //}

            bilal n = new bilal() { FirstName = "Faizan", SirName = "Ahmad" };

            List<bilal> list = new List<bilal>();

            list.Add(n ?? new bilal() {  FirstName = "bilal", SirName = "shahbaz"});

            Console.ReadLine();

        }

        public class bilal
        {
            public string FirstName { get; set; }
            public string SirName { get; set; }
        }

    }
}
