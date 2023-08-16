using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFDF
{
    public class Client
    {
        private static DataSource source = DataSource.Instance();
        private static string[] tickers; 

        public Client(string[] _tickers)
        {
            tickers = _tickers;
        }
        public void Request()
        {
            source.Post(tickers);
        }
    }
}
