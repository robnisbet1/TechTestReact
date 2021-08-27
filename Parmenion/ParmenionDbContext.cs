namespace Parmenion
{
    using Microsoft.EntityFrameworkCore;
    using Parmenion.Entities;

    public class ParmenionDbContext : DbContext
    {
        public ParmenionDbContext(DbContextOptions<ParmenionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contributions> Contributions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParmenionDbContext).Assembly);
        }
    }
}
