using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TyMiniProject.Models;

namespace TyMiniProject.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<FeeTable> FeeTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-One: Users ↔ Profile (shared PK)
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId);

            // One-to-Many: Profile ↔ FeeTables
            modelBuilder.Entity<Profile>()
                .HasMany(p => p.FeeTables)
                .WithOne(f => f.Profile)
                .HasForeignKey(f => f.UserId);
        }

    }
}
