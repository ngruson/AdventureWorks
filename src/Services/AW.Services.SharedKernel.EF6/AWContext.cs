using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace AW.Services.SharedKernel.EF6
{
    public partial class AWContext : DbContext
    {
        private readonly Assembly configurationAssembly;

        public AWContext()
            : base("AWContext")
        {
        }

        public AWContext(DbConnection existingConnection, bool contextOwnsConnection, Assembly configurationAssembly)
            : base(existingConnection, contextOwnsConnection)
        {
            this.configurationAssembly = configurationAssembly;
        }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = configurationAssembly.GetTypes()
              .Where(type => !string.IsNullOrEmpty(type.Namespace))
              .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                   && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}