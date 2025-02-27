using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        //public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Company>()
        //        .HasOne(c => c.CEO)
        //        .WithOne(u => u.Company)
        //        .HasForeignKey<Company>(c => c.CEOId);
        //}
    }
}
