using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Solitaire.DataAccess.Models;

namespace Solitaire.DataAccess.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.SolitaireSession)
                .WithOne(u => u.ApplicationUser);

            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<SolitaireSession> SolitaireSessions { get; set; }
    }
}
