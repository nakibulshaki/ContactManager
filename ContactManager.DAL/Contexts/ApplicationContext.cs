using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactMangaer.Data.Enums;
using ContactMangaer.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactManager.Data.Contexts
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ConfigureEntity();
           modelBuilder.SeedData();
        }
    }
}
