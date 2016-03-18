using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication29.Models
{
    public class FrendsModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        public virtual UserData UserA { get; set; }
        public virtual UserData UserB { get; set; }
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
        public bool IsConfirm { get; set; }
    }
}