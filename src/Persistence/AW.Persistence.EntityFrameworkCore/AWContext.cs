using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AW.Persistence.EntityFrameworkCore
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

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public DbSet<Customer> Customers { get; set; }
    }
}