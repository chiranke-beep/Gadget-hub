using TechWorld.Data;
using TechWorld.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TechWorld.Repo
{
    public interface IQuotationResponseRepo
    {
        Task SaveAsync(QuotationResponse entity);
        Task<QuotationResponse> GetByIdAsync(int id);
        Task UpdateAsync(QuotationResponse entity);
        Task DeleteAsync(QuotationResponse entity);
        Task<List<QuotationResponse>> GetAllAsync();
    }

    public class QuotationResponseRepo : IQuotationResponseRepo
    {
        private readonly ApplicationDbContext _context;

        public QuotationResponseRepo(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task SaveAsync(QuotationResponse entity)
        {
            _context.QuotationResponses.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<QuotationResponse> GetByIdAsync(int id)
        {
            return await _context.QuotationResponses.Include(q => q.ProductQuoteInfos).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task UpdateAsync(QuotationResponse entity)
        {
            _context.QuotationResponses.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(QuotationResponse entity)
        {
            _context.QuotationResponses.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<QuotationResponse>> GetAllAsync()
        {
            return await _context.QuotationResponses.Include(q => q.ProductQuoteInfos).ToListAsync();
        }
    }
}