using Dwagen.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Model
{
    public class DwagenContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public string ConnectionType = "DefaultConnection";
        public DwagenContext()
        {
        }
        public DwagenContext(DbContextOptions<DwagenContext> options) : base(options)
        {
        }

        /// <summary>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration[$"ConnectionStrings:{ConnectionType}"], options => options.EnableRetryOnFailure());
            }
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<UsersProfile> UsersProfile { get; set; }
        public DbSet<Rates> Rates { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData.Seed(builder);
        }
    }
}
