using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
﻿using Microsoft.EntityFrameworkCore;
using Social_Light_EMAIL.Models;

namespace Social_Light_EMAIL.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
         public DbSet<EmailLoggers> EmailLoggers { get; set; } 
    }
}