﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}