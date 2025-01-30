using System;
using BudgetTracker.Database.Entities;
using BudgetTracker.Utitlities.Enums;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Database.DbContexts;

public class BudgetTrackerDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<BudgetLimit> BudgetLimits { get; set; }
    public DbSet<RecurrenceIntervalEntity> recurrenceIntervalEntities { get; set; }
    public DbSet<Income> UserIncomes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public BudgetTrackerDbContext(DbContextOptions<BudgetTrackerDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
        .HasMany(r => r.Users)
        .WithOne(u => u.Role)
        .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "Admin" },
            new Role { RoleId = 2, RoleName = "RegularUser" }
        );
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryName = CategoryType.Food.ToString() },
            new Category { CategoryId = 2, CategoryName = CategoryType.Rent.ToString() },
            new Category { CategoryId = 3, CategoryName = CategoryType.Utilities.ToString() },
            new Category { CategoryId = 4, CategoryName = CategoryType.Entertainment.ToString() },
            new Category { CategoryId = 5, CategoryName = CategoryType.Transportation.ToString() },
            new Category { CategoryId = 6, CategoryName = CategoryType.HealthCare.ToString() }
        );

        modelBuilder.Entity<User>()
        .HasMany(u => u.Incomes)
        .WithOne(i => i.User)
        .HasForeignKey(i => i.UserId);

        modelBuilder.Entity<User>()
        .HasMany(u => u.Expenses)
        .WithOne(e => e.User)
        .HasForeignKey(u => u.UserId);

        modelBuilder.Entity<Expense>()
        .HasOne(e => e.Category)
        .WithMany()
        .HasForeignKey(b => b.CategoryId);

        modelBuilder.Entity<BudgetLimit>()
        .HasOne(b => b.Category)
        .WithMany()
        .HasForeignKey(b => b.CategoryId);

        modelBuilder.Entity<User>()
        .HasMany(b => b.BudgetLimits)
        .WithOne(u => u.User)
        .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<RecurrenceIntervalEntity>().HasData(
     new RecurrenceIntervalEntity { Id = 1, Name = RecurrenceInterval.Daily.ToString() },
     new RecurrenceIntervalEntity { Id = 2, Name = RecurrenceInterval.Weekly.ToString() },
     new RecurrenceIntervalEntity { Id = 3, Name = RecurrenceInterval.Monthly.ToString() },
     new RecurrenceIntervalEntity { Id = 4, Name = RecurrenceInterval.Yearly.ToString() }
 );

        modelBuilder.Entity<User>()
                       .HasMany(u => u.Notifications)
                       .WithOne(n => n.User)
                       .HasForeignKey(n => n.UserId);

        modelBuilder.Entity<BudgetLimit>()
            .HasMany(b => b.Notifications)
            .WithOne(n => n.BudgetLimit)
            .HasForeignKey(n => n.BudgetId);

    }
}
