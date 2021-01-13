using Desafio.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Repository.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}