using MvcApplication29.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication29.Models
{
    public class Wall
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        public string Title { get; set; }
        public string Text { get; set; }
        public string ContentUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public virtual UserProfile ThisUser { get; set;}
        public virtual UserProfile PostUser {get;set;}
        }
    }

    /*
     *   `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(128) NOT NULL,
  `text` text NOT NULL,
  `creation` datetime NOT NULL,
  `modification` datetime NOT NULL,
  `img` varchar(128) NOT NULL DEFAULT 'default.png',
  `status` tinyint(4) NOT NULL DEFAULT '2',
  `user_id` int(11) NOT NULL,
  `rate` int(11) NOT NULL,
  `relax_type` tinyint(4) NOT NULL,
  `timers` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `contest` tinyint(1) NOT NULL DEFAULT '0',
  `views` int(11) NOT NULL DEFAULT '0',
  `comment` int(11) NOT NULL,
  `url` varchar(128) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `url` (`url`),
  KEY `country_id` (`country_id`),
  KEY `user_id` (`user_id`),
  KEY `status` (`status`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=1456435 ;
     * */
