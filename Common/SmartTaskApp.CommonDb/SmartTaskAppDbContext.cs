using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartTaskApp.CommonDb.Entities;

namespace SmartTaskApp.CommonDb
{
    public class SmartTaskAppDbContext : IdentityDbContext<User>
    {
        public SmartTaskAppDbContext(DbContextOptions<SmartTaskAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("Roles");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("TaskItems");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Priority).IsRequired();
            });

            builder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Owner).IsRequired();
            });

            builder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notifications");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(500);
            });

            builder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired();
                entity.Property(e => e.ExpiresAt).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.RevokedAt);
            });
        }
    }
}
