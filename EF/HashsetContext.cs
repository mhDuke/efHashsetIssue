using EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    public class HashsetContext : DbContext
    {
        public HashsetContext(DbContextOptions options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleInitialized> VehiclesWithHashSetInitialized { get; set; }
        public DbSet<Strings> Strings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
