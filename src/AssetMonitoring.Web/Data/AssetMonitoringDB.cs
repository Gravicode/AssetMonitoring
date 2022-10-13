using Microsoft.EntityFrameworkCore;
using AssetMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetMonitoring.Web.Data;

namespace AssetMonitoring.Web.Data
{
    public class AssetMonitoringDB : DbContext
    {

        public AssetMonitoringDB()
        {
        }

        public AssetMonitoringDB(DbContextOptions<AssetMonitoringDB> options)
            : base(options)
        {
        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceRequest> DeviceRequests { get; set; }
        public DbSet<DeviceHistory> DeviceHistorys { get; set; }
        public DbSet<DeviceRepair> DeviceRepairs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*
            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
            builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);

            // shadow properties
            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");
            */
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            /*
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<SourceInfo>();
            updateUpdatedProperty<DataEventRecord>();
            */
            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(AppConstants.SQLConn, ServerVersion.AutoDetect(AppConstants.SQLConn));
            }
        }
        private void updateUpdatedProperty<T>() where T : class
        {
            /*
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
            */
        }

    }
}
