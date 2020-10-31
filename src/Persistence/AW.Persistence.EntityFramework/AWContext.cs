using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using AW.Persistence.EntityFramework.Configurations;

namespace AW.Persistence.EntityFramework
{
    public partial class AWContext : DbContext
    {
        public AWContext()
            : base("AWContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new BaseEntityConfiguration());

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
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