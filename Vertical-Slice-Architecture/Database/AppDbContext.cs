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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        #region DbSets

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Department> Departments { get; set; }

        #endregion

    }
}
