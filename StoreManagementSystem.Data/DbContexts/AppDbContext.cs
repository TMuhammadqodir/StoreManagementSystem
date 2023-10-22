using Microsoft.EntityFrameworkCore;
using StoreManagementSystem.Domain.Entities;
using System.Collections.Generic;

namespace StoreManagementSystem.Data.DbContexts;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        string connectionString = DbConstant.CONNECTION_STRING;

        optionsBuilder.UseMySQL(connectionString);
    }
    public DbSet<StoreManager> StoreManagers { get; set; }
    public DbSet<Store> Stories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region filter is delete
        modelBuilder.Entity<Store>().HasQueryFilter(u => !u.IsDelete);
        modelBuilder.Entity<StoreManager>().HasQueryFilter(u => !u.IsDelete);
        #endregion
    }
}

