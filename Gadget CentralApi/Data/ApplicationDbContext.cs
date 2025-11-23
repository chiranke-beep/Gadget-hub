using Microsoft.EntityFrameworkCore;
using Gadget_CentralApi.Models;

namespace Gadget_CentralApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<QuotationRequest> QuotationRequests { get; set; }
        public DbSet<QuotationResponse> QuotationResponses { get; set; }
        public DbSet<ProductQuoteInfo> ProductQuoteInfos { get; set; }
        public DbSet<OrderConfirmation> OrderConfirmations { get; set; }
        public DbSet<ConfirmedProduct> ConfirmedProducts { get; set; }
        public DbSet<ProductRequestInfo> ProductRequestInfos { get; set; } // Added
    }
}