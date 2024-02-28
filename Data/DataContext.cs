using dating_backend.Entities;
using Microsoft.EntityFrameworkCore;


namespace dating_backend.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        // Create DbSets Here... 
        public DbSet<User> Users { get; set; }
    }
}
