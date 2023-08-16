using System;

namespace SingletonDemo
{
    //Eager Loading
    public sealed class Singleton
    {
        private static int counter = 0;

        private Singleton()
        {
            counter++;
            Console.WriteLine("This Counter value is: " + counter);
        }

        //private static Singleton instance = null;
        private static readonly Lazy<Singleton> instance = new Lazy<Singleton>(() => new Singleton());
        
        //private static readonly object obj = new object();
        public static Singleton GetInstance
        {
            get
            {
                //if(instance == null)
                //{
                //    lock (obj)
                //    {
                //        if (instance == null)
                //            instance = new Singleton();
                //    }
                //}

                return instance.Value;
            }
        }
        
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }
    }
}
