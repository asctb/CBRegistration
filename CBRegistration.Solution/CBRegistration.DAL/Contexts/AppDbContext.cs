using CBRegistration.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.DAL.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .Property(b => b.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<UserEntity>()
                .Property(b => b.IsBiometricLoginEnabled)
                .HasDefaultValue(false);

            modelBuilder.Entity<UserEntity>()
                .Property(b => b.HasAcceptedTermsConditions)
                .HasDefaultValue(false);
        }
    }
}
