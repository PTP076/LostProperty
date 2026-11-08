using Microsoft.EntityFrameworkCore;
using FindIt.Models;

namespace FindIt.Data
{
    public class FindItContext : DbContext
    {
        public FindItContext(DbContextOptions<FindItContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}