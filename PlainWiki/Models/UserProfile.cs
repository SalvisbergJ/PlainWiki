using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlainWiki.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public int Password { get; set; }
    }
}
