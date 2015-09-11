using System.Data.Entity;

namespace ConsoleApplication2
{
    public class Context : DbContext
    {
        public Context() : base("default") { }
        public DbSet<Item> Items { get; set; }
    }
}