using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlainWiki.Models
{
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }

        public int Password { get; set; }
    }
}
