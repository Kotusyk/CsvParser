using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace App.Data
{
    public class TaskDBContext : DbContext
    {
        public TaskDBContext(DbContextOptions<TaskDBContext> options) : base(options)
        {
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired();

                entity.Property(u => u.DateOfBirth)
                .HasColumnType("date")
                .IsRequired();

                entity.Property(u => u.Married)
                .HasColumnType("bit")
                .IsRequired();

                entity.Property(u => u.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired();

                entity.Property(u => u.Salary)
                .HasColumnType("decimal")
                .IsRequired();

            });
        }
        public DbSet<User> Users { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(
        //     @"Server=(localdb)\mssqllocaldb;Database=AnnaTaskDB;Trusted_Connection=True", b => b.MigrationsAssembly("Presentation"));
        //    }
        //    // optionsBuilder
        //    //.UseSqlServer(
        //    //  @"Server=(localdb)\mssqllocaldb;Database=AnnaTaskDB;Trusted_Connection=True",
        //    //  options => options.EnableRetryOnFailure());
        //}

    }
}
