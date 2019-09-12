using System.Data.Entity;


namespace OrmTestSpeed
{
    public class Context : DbContext
    {
        public Context() : base("default") { }
        public DbSet<Item> Items { get; set; }
    }
}