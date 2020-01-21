using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class ApplicationDbContext : DbContext
    {
#nullable disable // Properties are initialize and checked in the set method.
        private DbSet<User> _Users;
        public DbSet<User> Users
        {
            get => _Users;
            set => _Users = value ?? throw new ArgumentNullException(nameof(value));
        }
        private DbSet<Group> _Groups;
        public DbSet<Group> Groups
        {
            get => _Groups;
            set => _Groups = value ?? throw new ArgumentNullException(nameof(value));
        }
        private DbSet<Gift> _Gifts;
        public DbSet<Gift> Gifts
        {
            get => _Gifts;
            set => _Gifts = value ?? throw new ArgumentNullException(nameof(value));
        }
        private IHttpContextAccessor _HttpContextAccessor;
        public IHttpContextAccessor HttpContextAccessor
        {
            get => _HttpContextAccessor;
            set => _HttpContextAccessor = value ?? throw new ArgumentNullException(nameof(value));
        }
#nullable enable

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContectAccessor) : base(options)
        {
            HttpContextAccessor = httpContectAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null) throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Entity<GroupData>().HasKey(pt => new { pt.UserId, pt.GroupId });

            modelBuilder.Entity<GroupData>()
                .HasOne(gd => gd.User)
                .WithMany(gd => gd.GroupData)
                .HasForeignKey(gd => gd.UserId);

            modelBuilder.Entity<GroupData>()
                .HasOne(gd => gd.Group)
                .WithMany(gd => gd.GroupData)
                .HasForeignKey(gd => gd.GroupId);
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
            IEnumerable<EntityEntry> modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            IEnumerable<EntityEntry> added = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            foreach (EntityEntry entry in added)
            {
                var fingerPrintEntry = entry.Entity as FingerPrintEntityBase;
                if(!(fingerPrintEntry is null))
                {
                    fingerPrintEntry.CreatedOn = DateTime.UtcNow;
                    fingerPrintEntry.CreatedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                }
            }

            foreach (EntityEntry entry in modified)
            {
                var fingerPrintEntry = entry.Entity as FingerPrintEntityBase;
                if (!(fingerPrintEntry is null))
                {
                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                }
            }
        }
    }
}
