using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptov2.Models
{
    public class Users
    {
        public Int32 id { get; set; }
        public String username { get; set; }
        public String senha { get; set; }
        public Int32 apiKey { get; set; }
    }
}
