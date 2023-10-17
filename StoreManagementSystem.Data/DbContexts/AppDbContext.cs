using Microsoft.EntityFrameworkCore;
using StoreManagementSystem.Domain.Entities;
using System.Collections.Generic;

namespace StoreManagementSystem.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<StoreManager> StoreManagers { get; set; }
    public DbSet<Story> Stories { get; set; }
}

