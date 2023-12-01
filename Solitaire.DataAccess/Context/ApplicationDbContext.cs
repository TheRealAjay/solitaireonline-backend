using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Solitaire.Models;

namespace Solitaire.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.SolitaireSession)
                .WithOne(u => u.ApplicationUser);

            builder.Entity<SolitaireSession>()
                .Property(p => p.Id)
                .UseIdentityColumn();

            builder.Entity<SolitaireSession>()
                .HasMany(s => s.Cards)
                .WithOne(c => c.SolitaireSession);

            builder.Entity<Card>()
                .Property(p => p.Id)
                .UseIdentityColumn();

            builder.Entity<SolitaireSession>()
                .HasMany(s => s.Draws)
                .WithOne(d => d.SolitaireSession);

            builder.Entity<Draw>()
                .Property(p => p.Id)
                .UseIdentityColumn();

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(_configuration.GetConnectionString("Solitaire") ?? string.Empty);
        }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<SolitaireSession> SolitaireSessions { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Draw> Draws { get; set; }
    }
}
