using TechWorld.Data;
using TechWorld.Models;
using Microsoft.EntityFrameworkCore;

namespace TechWorld.Repo
{
    public interface IProductQuoteInfoRepo
    {
        Task SaveAsync(List<ProductQuoteInfo> entities);
    }

    public class ProductQuoteInfoRepo : IProductQuoteInfoRepo
    {
        private readonly ApplicationDbContext _context;

        public ProductQuoteInfoRepo(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task SaveAsync(List<ProductQuoteInfo> entities)
        {
            _context.ProductQuoteInfos.AddRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}