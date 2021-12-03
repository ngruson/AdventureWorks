using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AW.Services.SharedKernel.EFCore.UnitTests.TestData
{
    public class ItemsContext : AWContext
    {
        public ItemsContext(DbContextOptions<AWContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(
                b =>
                {
                    b.Property(t => t.Id);
                    b.HasKey(t => t.Id);
                    b.Property(t => t.Name);
                });
        }
    }
}