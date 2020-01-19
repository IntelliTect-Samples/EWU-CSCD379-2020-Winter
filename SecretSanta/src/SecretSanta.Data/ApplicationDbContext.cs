using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSanta.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public IHttpContextAccessor Accessor { get; set; }
#nullable disable
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor accessor) : base(options)
        {
            Accessor = accessor;
        }
#nullable enable
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<UserGroup>().HasKey(g => new { g.UserId, g.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(x => x.Group)
                .WithMany(x => x.UserGroups)
                .HasForeignKey(x => x.GroupId);
        }

        public override int SaveChanges()
        {
            AddFingerPrinting();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddFingerPrinting();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddFingerPrinting()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            var added = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            foreach (var e in modified)
            {
                var fingerPrintEntry = e.Entity as FingerPrintEntityBase;
                if (fingerPrintEntry != null)
                {
                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = Accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                }
            }

            foreach (var e in added)
            {
                var fingerPrintEntry = e.Entity as FingerPrintEntityBase;
                if (fingerPrintEntry != null)
                {
                    fingerPrintEntry.CreatedOn = DateTime.UtcNow;
                    fingerPrintEntry.CreatedBy = Accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = Accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = Accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                }
            }
        }
    }
}
