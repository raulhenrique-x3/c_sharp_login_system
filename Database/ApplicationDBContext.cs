using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_USER.Models;
using Microsoft.EntityFrameworkCore;

namespace API_USER.Database
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(key => key.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(255);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(255);
                entity.Property(p => p.Password).IsRequired().HasMaxLength(255);
                entity.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}