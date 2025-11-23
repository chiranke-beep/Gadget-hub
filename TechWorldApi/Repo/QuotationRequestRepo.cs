using System.Collections.Generic;
using System.Threading.Tasks;
using TechWorld.Data;
using TechWorld.Models;
using Microsoft.EntityFrameworkCore;

namespace TechWorld.Repo
{
    public class QuotationRequestRepo : IQuotationRequestRepo
    {
        private readonly ApplicationDbContext _context;

        public QuotationRequestRepo(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task SaveAsync(QuotationRequest quotationRequest)
        {
            _context.QuotationRequests.Add(quotationRequest);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(List<ProductRequestInfo> productRequestInfos)
        {
            _context.ProductRequestInfos.AddRange(productRequestInfos);
            await _context.SaveChangesAsync();
        }

        public async Task<QuotationRequest> GetByIdAsync(int id)
        {
            return await _context.QuotationRequests
                .Include(q => q.ProductRequestInfos)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task UpdateAsync(QuotationRequest quotationRequest)
        {
            _context.QuotationRequests.Update(quotationRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(QuotationRequest quotationRequest)
        {
            _context.QuotationRequests.Remove(quotationRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<QuotationRequest>> GetAllAsync()
        {
            return await _context.QuotationRequests.Include(q => q.ProductRequestInfos).ToListAsync();
        }
    }
}