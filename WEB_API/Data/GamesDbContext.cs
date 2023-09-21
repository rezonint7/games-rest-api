using Microsoft.EntityFrameworkCore;
using WEB_API.Models;

namespace WEB_API.Data
{
    public class GamesDbContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Info> Info { get; set; }
        public DbSet<SystemRequirements> SystemRequirements { get; set; }

        public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options) { }
        public GamesDbContext() : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Category>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

            });
            modelBuilder.Entity<Game>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id);
                entity.Property(e => e.CategoryId);
                entity.HasOne(e => e.Category)
                    .WithMany()
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Info>(entity => {
                entity.HasKey(e => e.GameId);
                entity.Property(e => e.GameId);
                entity.HasOne(e => e.Game)
                    .WithOne(g => g.Info)
                    .HasForeignKey<Info>(e => e.GameId);
            });
            modelBuilder.Entity<SystemRequirements>(entity => {
                entity.HasKey(e => e.GameId);
                entity.Property(e => e.GameId);
                entity.HasOne(e => e.Game)
                    .WithOne(g => g.SystemRequirements)
                    .HasForeignKey<SystemRequirements>(e => e.GameId);
            });
        }
    }
}
