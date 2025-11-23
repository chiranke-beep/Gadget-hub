using Gadget_CentralApi.Data;
using Gadget_CentralApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gadget_CentralApi.Repo
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