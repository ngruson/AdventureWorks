using AW.Services.SharedKernel.EFCore.UnitTests.TestData;
using System.Data.Common;
using System.Data.Entity;
using System.Reflection;

namespace AW.Services.SharedKernel.EF6.UnitTests.TestData
{
    public class ItemsContext : AWContext
    {
        public ItemsContext(DbConnection existingConnection, bool contextOwnsConnection, Assembly configurationAssembly)
            : base(existingConnection, contextOwnsConnection, configurationAssembly)
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}