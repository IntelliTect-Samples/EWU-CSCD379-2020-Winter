using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSanta.Data
{
    public class ApplicationDbContext : DbContext
    {
#nullable disable
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
#nullable enable

        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options,
                IHttpContextAccessor httpContextAccessor) : base(options)
        {
            HttpContextAccessor = httpContextAccessor
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null) throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Entity<UserGroup>()
                .HasKey(pt => new { pt.UserId, pt.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);
        }

        public override int SaveChanges()
        {
            AddFingerPrinting();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(
                CancellationToken cancellationToken = default)
        {
            AddFingerPrinting();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddFingerPrinting()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            var added = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            foreach (var entry in added)
            {
                if (entry.Entity is FingerPrintEntityBase fingerprinted)
                {
                    fingerprinted.CreatedOn = DateTime.UtcNow;
                    fingerprinted.CreatedBy = HttpContextAccessor?.HttpContext?.User
                        ?.FindFirst(ClaimTypes.NameIdentifier)
                        .Value ?? "";
                    fingerprinted.ModifiedOn = DateTime.UtcNow;
                    fingerprinted.ModifiedBy = HttpContextAccessor?.HttpContext?.User?.
                        FindFirst(ClaimTypes.NameIdentifier)
                        .Value ?? "";
                }
            }

            foreach (var entry in modified)
            {
                if (entry.Entity is FingerPrintEntityBase fingerprinted)
                {
                    fingerprinted.ModifiedOn = DateTime.UtcNow;
                    fingerprinted.ModifiedBy = HttpContextAccessor?.HttpContext?.User?.
                        FindFirst(ClaimTypes.NameIdentifier)
                        .Value ?? "";
                }
            }
        }
    }
}
