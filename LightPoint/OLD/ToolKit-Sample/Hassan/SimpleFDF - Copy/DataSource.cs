using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFDF
{
    public class DataSource
    {
        private static DataSource obj = null;
        private static object sync = new object();

        private BlockingCollection<string[]> requests;

        private DataSource()
        {
            requests = new BlockingCollection<string[]>();
        }

        public static DataSource Instance()
        {
            if(obj == null)
            {
                lock(sync)
                {
                    if(obj == null)
                    {
                        obj = new DataSource();
                    }
                }
            }

            return obj;
        }

        public void Post(string[] tickers)
        {
            requests.TryAdd(tickers);
        }

        public string[] Recieve()
        {
            return requests.Take();
        }
    }
}
