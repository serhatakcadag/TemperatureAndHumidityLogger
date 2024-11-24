using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Entities.Logs;
using TemperatureAndHumidityLogger.Core.Entities.Users;

namespace TemperatureAndHumidityLogger.Infrastructure.EFCore
{
    public class EfDbContext : IdentityDbContext<User, Role, Guid>
    {
        public EfDbContext(DbContextOptions<EfDbContext> options)
        : base(options) { }

        // DbSet properties for each entity
        public DbSet<Log> Logs { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Adding a global filter for soft delete
            modelBuilder.Entity<User>().HasQueryFilter(u => u.DeletedAt == null);
            modelBuilder.Entity<Device>().HasQueryFilter(d => d.DeletedAt == null);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Devices)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Device>()
                .HasMany(d => d.Logs)
                .WithOne(l => l.Device)
                .HasForeignKey(l => l.DeviceId);

            modelBuilder.Entity<Device>()
                .HasIndex(d => d.SerialNumber)
                .IsUnique();
        }
    }
}
