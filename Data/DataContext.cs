using dating_backend.Entities;
using Microsoft.EntityFrameworkCore;


namespace dating_backend.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        // Create DbSets Here... 
        public DbSet<User> Users { get; set; }
        public DbSet<UserLike> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLike>()
                .HasKey(k => new { k.SourceUserId, k.TargetUserId });

            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserLike>()
               .HasOne(s => s.TargetUser)
               .WithMany(l => l.LikedByUsers)
               .HasForeignKey(s => s.TargetUserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
