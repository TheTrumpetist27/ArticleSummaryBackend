global using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<ArticleEntity> Articles => Set<ArticleEntity>();
        public DbSet<SourceEntity> Sources => Set<SourceEntity>();
        public DbSet<CommentEntity> Comments => Set<CommentEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleEntity>()
                .HasOne(a => a.Source)
                .WithOne(s => s.Article)
                .HasForeignKey<SourceEntity>(s => s.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CommentEntity>()
                .HasOne(c => c.Article)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
