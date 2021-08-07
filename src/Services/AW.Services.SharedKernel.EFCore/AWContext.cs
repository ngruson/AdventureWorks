using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AW.Services.SharedKernel.EFCore
{
    public class AWContext : DbContext
    {
        private readonly Assembly configurationsAssembly;
        public AWContext()
            : base()
        {
        }

        public AWContext(DbContextOptions<AWContext> options, Assembly configurationsAssembly) : base(options)
        {
            this.configurationsAssembly = configurationsAssembly;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(configurationsAssembly);
        }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}