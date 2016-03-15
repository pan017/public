using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication29.Models
{
    public class Message
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual UserProfile UserGet { get; set; }
        public virtual UserProfile UserPost { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public bool IsRead { get; set; }

        
    }
}