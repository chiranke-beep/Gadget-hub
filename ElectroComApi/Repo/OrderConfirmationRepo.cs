using System.Collections.Generic;
using System.Threading.Tasks;
using ElectroComApi.Data;
using ElectroComApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroComApi.Repo
{
    public class OrderConfirmationRepo : IOrderConfirmationRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderConfirmationRepo(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task SaveAsync(OrderConfirmation orderConfirmation)
        {
            _context.OrderConfirmations.Add(orderConfirmation);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(List<ConfirmedProduct> confirmedProducts)
        {
            _context.ConfirmedProducts.AddRange(confirmedProducts);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderConfirmation> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderConfirmations
                .Include(o => o.ConfirmedProducts)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task UpdateAsync(OrderConfirmation orderConfirmation)
        {
            _context.OrderConfirmations.Update(orderConfirmation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OrderConfirmation orderConfirmation)
        {
            _context.OrderConfirmations.Remove(orderConfirmation);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderConfirmation>> GetAllAsync()
        {
            return await _context.OrderConfirmations.Include(o => o.ConfirmedProducts).ToListAsync();
        }
    }
}