using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication15.Models
{
    public class UserData
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BrithDay { get; set; }
        public string Sex { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string HPhone { get; set; }
        public string Skype { get; set; }
        public string WebSite { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string School { get; set; }
        public string College { get; set; }
        public string Institute { get; set; }
        public string Job { get; set; }
        public string Interesses { get; set; }
        public string FavoriteBook { get; set; }
        public string FavoriteMusik { get; set; }
        public string FavoriteKino { get; set; }
        public string Entertainment { get; set; }//Развлечения  
        public string FavoriteGames { get; set; }
        public string About { get; set; }
        public string AvatarUrl { get; set; }
    }
    public class Communication
    {
        [Key]
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public int UserDataId { get; set; }
    }

}