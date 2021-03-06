using Desafio.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            ILoggerFactory logger = LoggerFactory.Create(c => c.AddConsole());
            optionsBuilder.UseLoggerFactory(logger);

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}