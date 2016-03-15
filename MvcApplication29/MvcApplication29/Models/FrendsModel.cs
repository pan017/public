using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication29.Models
{
    public class FrendsModel
    {
        public int Id { get; set;}
        public virtual UserData UserA { get; set; }
        public virtual UserData UserB { get; set; }
        public DateTime Time { get; set; }
        public bool IsConfirm { get; set; }
    }
}