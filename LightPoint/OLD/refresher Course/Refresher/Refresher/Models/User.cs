using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refresher.Models
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public double Points { get; set; }
        public ProfileDetail ProfileDetail { get; set; }
    }
}
