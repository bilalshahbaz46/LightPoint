using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refresher.Models
{
    public class ProfileDetail 
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Age { get; set; }
        public string? Address { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
