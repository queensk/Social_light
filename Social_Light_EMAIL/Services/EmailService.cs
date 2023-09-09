using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social_Light_EMAIL.Data;
using Social_Light_EMAIL.Models;

namespace Social_Light_EMAIL.Services
{
    public class EmailService
    {
        private DbContextOptions<AppDbContext> options;

        public EmailService()
        {
        }

        public EmailService(DbContextOptions<AppDbContext> options)
        {
            this.options = options;
        }

        public async Task SaveData(EmailLoggers emailLoggers)
        {
            var _context = new AppDbContext(options);
            _context.EmailLoggers.Add(emailLoggers);
            await _context.SaveChangesAsync();
        }
    }
}