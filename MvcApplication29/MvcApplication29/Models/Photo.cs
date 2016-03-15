using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication29.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public virtual UserProfile UserProfile { get; set;}
        public string Description { get; set; }
        public string Url { get; set; }
    }
}