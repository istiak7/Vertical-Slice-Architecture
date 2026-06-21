using Microsoft.EntityFrameworkCore;
using Vertical_Slice_Architecture.Entities;

namespace Vertical_Slice_Architecture.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Configure your entities and relationships here
            modelBuilder.Entity<Activity>(entity =>
            {
               entity.HasKey(e => e.Id);
               entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
               entity.Property(e => e.Description).HasMaxLength(500);
            });
        }

        #region DbSets

        public DbSet<Activity> Activities { get; set; }

        #endregion

    }
}
