using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecretSanta.Data
{
    public class ApplicationDbContext : DbContext
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //implement
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();//implement
        }

        //public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        //{

        //}
    }
}
