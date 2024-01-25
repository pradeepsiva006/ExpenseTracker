using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.DataAccess.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<User> Users { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config) : base(options)
        {
            _configuration = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ExpenseTracker"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(255); 

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Expenses_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserExpenses)
                    .HasForeignKey(d => d.UserId);

                entity.Property(e => e.Amount)
                      .HasColumnType("decimal(18, 2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
