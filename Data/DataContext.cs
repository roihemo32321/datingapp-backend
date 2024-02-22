using dating_backend.Entities;
using Microsoft.EntityFrameworkCore;


namespace dating_backend.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        // Created an DbSet called Users(name of the database by EF) and used our User Entity to provide our columns names.
        public DbSet<User> Users { get; set; }
    }
}
