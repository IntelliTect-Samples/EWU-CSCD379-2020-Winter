using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEngine.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Post> Posts { get; private set; }
        public DbSet<Author> Authors { get; private set; }
        public DbSet<Comment> Comments { get; private set; }
        public DbSet<Tag> Tags { get; private set; }
        public DbSet<PostTag> PostTags { get; private set; }
        public IHttpContextAccessor HttpContextAccessor { get; private set; }

// Justifiction: Properties initialized by Entity Framework.
#nullable disable // CS8618: Non-nullable field is uninitialized. Consider declaring as nullable.
        public ApplicationDbContext(
#nullable enable
            DbContextOptions<ApplicationDbContext> options) : base(options) { }

// Justifiction: Properties initialized by Entity Framework.
#nullable disable // CS8618: Non-nullable field is uninitialized. Consider declaring as nullable.
        public ApplicationDbContext(
#nullable enable

                DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : 
            base(options)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<PostTag>().HasKey(postTag => new { postTag.PostId, postTag.TagId });

            modelBuilder.Entity<PostTag>()
                .HasOne(postTag => postTag.Post)
                .WithMany(post => post!.PostTags)
                .HasForeignKey(postTag => postTag.PostId);

            modelBuilder.Entity<PostTag>()
                .HasOne(postTag => postTag.Tag)
                .WithMany(tag => tag!.PostTags)
                .HasForeignKey(postTag => postTag.TagId);
        }

        public override int SaveChanges()
        {
            AddFingerPrinting();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
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
                if (entry.Entity is FingerPrintEntityBase fingerPrintEntry)
                {
                    fingerPrintEntry.CreatedOn = DateTime.UtcNow;
                    fingerPrintEntry.CreatedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                }
            }

            foreach (var entry in modified)
            {
                if (entry.Entity is FingerPrintEntityBase fingerPrintEntry)
                {
                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? "";
                }
            }
        }
    }
}
