using Microsoft.EntityFrameworkCore;
using paySimplexData.Entities;

namespace paySimplexData.Context
{
    public class paySimplexContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Task> Task { get; set; }

        public paySimplexContext(DbContextOptions<paySimplexContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.Id });
            modelBuilder.Entity<State>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Task>().HasKey(x => new { x.Id });
        }
    }
}
