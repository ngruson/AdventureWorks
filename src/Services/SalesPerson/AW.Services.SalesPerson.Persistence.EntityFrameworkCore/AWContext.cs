using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AW.Services.SalesPerson.Persistence.EntityFrameworkCore
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
    }
}