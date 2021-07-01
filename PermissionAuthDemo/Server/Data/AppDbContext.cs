using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermissionAuthDemo.Server.Data.Entities;
using System.Linq;

namespace PermissionAuthDemo.Server.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, AppRoleClaim, IdentityUserToken<string>>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "AppUser", "Identity");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<AppRole>(entity =>
            {
                entity.ToTable(name: "Role", "Identity");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRole", "Identity");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaim", "Identity");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogin", "Identity");
            });

            builder.Entity<AppRoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaim", "Identity");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserToken", "Identity");
            });
        }
    }

}
