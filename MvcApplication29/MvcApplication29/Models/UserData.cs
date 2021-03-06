﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace MvcApplication29.Models
{
        [Table("UsersData")]
        
        public class UserData
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int UserId { get; set; }
            [Display (Name = "Ваше имя")]
            public string Name { get; set; }
            [Display(Name = "Ваша фамилия")]
            
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.Date)]
            //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            [Display(Name = "Дата рождения")]
            public DateTime BrithDay { get; set; }
            [Display(Name = "Пол")]
            public string Sex { get; set; }
            [Display(Name = "Родной город")]
            public string City { get; set; }
            [Display(Name = "Номер телефона")]
            public string Phone { get; set; }
            [Display(Name = "Номер домашнего телефона")]
            public string HPhone { get; set; }
            [Display(Name = "Skype")]
            public string Skype { get; set; }
            [Display(Name = "Web-site")]
            public string WebSite { get; set; }
            [Display(Name = "Twitter")]
            public string Twitter { get; set; }
            [Display(Name = "Instagram")]
            public string Instagram { get; set; }
            [Display(Name = "Ваша школа")]
            public string School { get; set; }
            [Display(Name = "Место получения средне-специального, средне-технического образования")]
            public string College { get; set; }
            [Display(Name = "Место получения высшего образования")]
            public string Institute { get; set; }
            [Display(Name = "Место работы")]
            public string Job { get; set; }
            [Display(Name = "Ваши интересы")]
            public string Interesses { get; set; }
            [Display(Name = "Любимые книги")]
            public string FavoriteBook { get; set; }
            [Display(Name = "Любимая музыка")]
            public string FavoriteMusik { get; set; }
            [Display(Name = "Любимые фильмы")]
            public string FavoriteKino { get; set; }
            [Display(Name = "Ваши развлечения")]
            public string Entertainment { get; set; }//Развлечения  
            [Display(Name = "Любимые игры")]
            public string FavoriteGames { get; set; }
            [Display(Name = "О себе")]
            public string About { get; set; }

            public string AvatarUrl { get; set; }
            public virtual UserProfile UserProfile { get; set; }

            public UserData() { }
            public UserData(UserProfile UserProfile)
            {

                
                this.AvatarUrl = "/Content/images/None-Avatar.jpg";
                this.BrithDay = DateTime.Now;
             
                this.UserProfile = UserProfile;
            }
        }
    }
