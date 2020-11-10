
using System;
using System.Collections.Generic;
using System.Text;
using FullStack.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace FullStack.Data
{
    public class FullStackDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<City> Cities { get; set; }

        public FullStackDbContext(DbContextOptions<FullStackDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}