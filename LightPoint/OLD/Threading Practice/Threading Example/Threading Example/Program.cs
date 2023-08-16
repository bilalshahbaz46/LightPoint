using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_Example
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Hello World!");

        //    Thread obj1 = new Thread(function1);
        //    //Thread obj2 = new Thread(function2);

        //    obj1.Start();
        //    //obj2.Start();


        //    Console.WriteLine("The Main program has ended");
        //    //function1();
        //    //function2();
        //}

        //static void function1()
        //{
        //        Console.WriteLine("The function1 is executing...");
        //        Console.ReadLine();
        //        Console.WriteLine("The function1 has executed!");
        //}

        //static void function2()
        //{
        //    for (int i = 0; i <= 10; i++)
        //    {
        //        Console.WriteLine("function 2 : " + i.ToString());
        //        Thread.Sleep(1500);
        //    }
        //}

        ///////////////////////////////////////////////////////////////////////////////////////////

        //static async Task Main(string[] args)
        //{
        //    await makeTea();
        //}

        //static async Task makeTea()
        //{
        //    var BW = boilWater();

        //    Console.WriteLine("Get the cups");
        //    Console.WriteLine("Put Tea into the cups");
        //    await BW;
        //    Console.WriteLine("Put Boiled water into the cups");
        //    Console.WriteLine("Enjoy your tea!");
        //}

        //static async Task<string> boilWater()
        //{
        //    Console.WriteLine("Put water into the kettle");
        //    Console.WriteLine("Push the heat button on the Kettle");
        //    await Task .Delay(4000);
        //    Console.WriteLine("The water is boiled!!!!!!");

        //    return "Boiled Water";
        //}

        //////////////////////////////////////////////////////////////////////////////////////////
        
        //static async Task Main(string[] args)
        //{
        //    //var f = function1();
        //    //Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString() + " = " + 1);
        //    ////var client = new HttpClient();
        //    //Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString() + " = " + 2);
        //    ////var task = client.GetStringAsync("https://www.google.com/");
        //    //Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString() + " = " + 3);
        //    //await f;
        //    //Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString() + " = " + 4);

        //    Thread obj1 = new Thread(AFunction);
        //    Thread obj2 = new Thread(BFunction);

        //    obj1.Start();
        //    obj2.Start();
        //}

        //static async Task function1()
        //{
        //    Console.WriteLine("Started the function1");
        //    await Task.Delay(10000);
        //    Console.WriteLine("Finished the function1");
        //}


        //static void AFunction()
        //{
        //    Console.WriteLine("The AFunction starts");
        //    Thread.Sleep(2000);
        //    Console.WriteLine("The AFunction ends");
        //}
        //static void BFunction()
        //{
        //    Console.WriteLine("The BFunction starts");
        //    Thread.Sleep(1500);
        //    Console.WriteLine("The BFunction ends");
        //}


        static void Main(string[] args)
        {
            Console.WriteLine("Main Starts");
            Account accountA = new Account(5000, 101);
            Account accountB = new Account(3000, 102);

            AccountManager managerA = new AccountManager(accountA, accountB, 1000);
            Thread T1 = new Thread(managerA.Transfer);
            T1.Name = "T1";

            AccountManager managerB = new AccountManager(accountB, accountA, 2000);
            Thread T2 = new Thread(managerB.Transfer);
            T2.Name = "T2";

            T1.Start();
            T2.Start();

            T1.Join();
            T2.Join();
            Console.WriteLine("Main Program completed");
        }
    }
}
