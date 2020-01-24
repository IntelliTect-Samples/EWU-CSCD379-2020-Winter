using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data
{
    
	public class ApplicationDbContext : DbContext
	{
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        public IHttpContextAccessor? HttpContextAccessor { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            HttpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });

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

        private void AddFingerPrinting()
        {
            IEnumerable<EntityEntry> modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            IEnumerable<EntityEntry> added = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            foreach (EntityEntry entry in added)
            {
                if (entry.Entity is FingerPrintEntityBase fingerPrintEntry)
                {
                    fingerPrintEntry.CreatedOn = DateTime.UtcNow;
                    fingerPrintEntry.CreatedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                }
            }

            foreach (EntityEntry entry in modified)
            {
                if (entry.Entity is FingerPrintEntityBase fingerPrintEntry)
                {
                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                }
            }
        }

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            AddFingerPrinting();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
