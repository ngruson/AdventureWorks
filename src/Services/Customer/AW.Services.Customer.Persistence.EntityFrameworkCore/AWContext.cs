using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AW.Services.Customer.Persistence.EntityFrameworkCore
{
    public class AWContext : DbContext
    {
        public AWContext()
            : base()
        {
        }

        public AWContext(DbContextOptions<AWContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}