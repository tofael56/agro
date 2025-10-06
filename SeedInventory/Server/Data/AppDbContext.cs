using Microsoft.EntityFrameworkCore;
using SeedInventory.Shared.Models;
using System.Collections.Generic;

namespace SeedInventory.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Seed> Seeds => Set<Seed>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
    }
}
