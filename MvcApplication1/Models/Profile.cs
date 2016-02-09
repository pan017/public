using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Profile
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Id_User { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public string MPhone { get; set; }
        [Required]
        public string HPhone { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Skype { get; set; }
        [Required]
        public string WibSite { get; set; }
    }
}