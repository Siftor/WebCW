using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCW.Models
{
    public class Ann
    {
        public int id { get; set; }

        public string description { get; set; }

        public bool isSeen { get; set;}

        public virtual ApplicationUser User { get; set; }
    }
}