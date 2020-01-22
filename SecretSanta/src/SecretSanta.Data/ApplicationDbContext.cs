using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Data
{

    public class ApplicationDbContext : DbContext
    {

        public DbSet<User>  Users  { get; set; }
        public DbSet<Gift>  Gifts  { get; set; }
        public DbSet<Group> Groups { get; set; }

        private IHttpContextAccessor Accessor { get; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor accessor) :
            base(options)
        {
            Accessor = accessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Entity<UserGroupRelation>().HasKey(relation => new {relation.UserId, relation.GroupId});

            builder.Entity<UserGroupRelation>()
                   .HasOne(relation => relation.User)
                   .WithMany(group => group.Relations)
                   .HasForeignKey(relation => relation.GroupId);

            builder.Entity<UserGroupRelation>()
                   .HasOne(relation => relation.Group)
                   .WithMany(group => group.Relations)
                   .HasForeignKey(relation => relation.GroupId);
        }

        public override int SaveChanges()
        {
            ApplyFingerPrinting();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ApplyFingerPrinting();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyFingerPrinting()
        {
            var modified = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Modified);
            var added    = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added);

            foreach (var entry in added)
            {
                if (!(entry.Entity is FingerPrintEntityBase fingerPrint)) continue;

                fingerPrint.CreatedOn = DateTime.UtcNow;
                fingerPrint.CreatedBy = Accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";

                fingerPrint.ModifiedOn = DateTime.UtcNow;
                fingerPrint.ModifiedBy = Accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
            }

            foreach (var entry in modified)
            {
                if (!(entry.Entity is FingerPrintEntityBase fingerPrint)) continue;

                fingerPrint.ModifiedOn = DateTime.UtcNow;
                fingerPrint.ModifiedBy = Accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
            }
        }

    }

}
