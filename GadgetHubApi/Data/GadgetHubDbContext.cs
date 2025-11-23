using Microsoft.EntityFrameworkCore;
using GadgetHub.Models;

namespace GadgetHub.Data
{
    public class GadgetHubDbContext : DbContext
    {
        public GadgetHubDbContext(DbContextOptions<GadgetHubDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductDistributorMap> ProductDistributorMaps { get; set; }
        public DbSet<DistributorResponse> DistributorResponses { get; set; }
        public DbSet<FinalResponse> FinalResponses { get; set; }
        public DbSet<FullyComparedResponse> FullyComparedResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure decimal precision for Price fields
            modelBuilder.Entity<FinalResponse>()
                .Property(f => f.Price)
                .HasColumnType("decimal(18,2)");
                
            modelBuilder.Entity<FullyComparedResponse>()
                .Property(f => f.Price)
                .HasColumnType("decimal(18,2)");
                
            modelBuilder.Entity<Order>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,2)");
                
            Data.ProductSeeder.SeedProducts(modelBuilder);
            // Make User navigation property in Order not required
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
