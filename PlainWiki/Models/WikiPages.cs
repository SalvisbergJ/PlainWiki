using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace PlainWiki.Models
{
    public class WikiPages
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string RichText { get; set; }
        //public List<Images> ImagesList { get; set; }
    }
}
