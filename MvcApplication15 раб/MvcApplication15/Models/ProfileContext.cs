using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication15.Models
{
    public class ProfileContext : DbContext
    {
        public ProfileContext()
            : base("fghy")
        {
        }
        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<Communication> Communications { get; set; }
    }
}