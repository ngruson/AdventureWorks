using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AW.Persistence.EntityFrameworkCore
{
    public class AWContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}