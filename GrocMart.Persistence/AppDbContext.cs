using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace GrocMart.Persistence
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Data.Users> Users { get; set; }
        public DbSet<Data.Products> Products { get; set; }
        public DbSet<Data.Cart> Cart { get; set; }
        public DbSet<Data.Orders> Orders { get; set; }
        public DbSet<Data.OrderItems> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var t = typeof(AppDbContext);
            modelBuilder.ApplyConfigurationsFromAssembly(t.Assembly);
            base.OnModelCreating(modelBuilder);

        }
    }
}
