﻿global using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleEntity>()
                .HasOne(a => a.Source)
                .WithOne(s => s.Article)
                .HasForeignKey<SourceEntity>(s => s.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
