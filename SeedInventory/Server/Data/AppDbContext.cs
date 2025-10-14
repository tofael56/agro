using Microsoft.EntityFrameworkCore;
using SeedInventory.Shared.Models;
using SeedInventory.Shared.Models.Setting;
using System.Collections.Generic;

namespace SeedInventory.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Seed> Seeds => Set<Seed>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<CompanyModel> Company => Set<CompanyModel>();
        public DbSet<DistributorModel> Distributors => Set<DistributorModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seed>()
                .Property(s => s.Quantity)
                .HasDefaultValue(0);


            modelBuilder.Entity<Seed>()
                .HasOne(t => t.Supplier)
                .WithMany()
                .HasForeignKey(t => t.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
