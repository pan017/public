using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int Role { get; set; }
    }
}