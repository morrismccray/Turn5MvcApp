using System.Data.Entity;

namespace Turn5MvcApp.Models
{
    public class ActualDatabaseContext : DbContext
    {
        public ActualDatabaseContext()
            : base("ActualDatabase") 
        {
        }

        public DbSet<SearchLog> SearchLog { get; set; }
    }
}