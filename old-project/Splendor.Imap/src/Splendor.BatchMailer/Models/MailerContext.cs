using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splendor.BatchMailer.Models {
    public class MailerContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=System.Data.dll");
        }
        public DbSet<Mail> Mails { get; set; }
    }
    public class Mail {
        public string Id { get; set; }

    }
}
