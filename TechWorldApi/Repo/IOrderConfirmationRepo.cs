using System.Collections.Generic;
using System.Threading.Tasks;
using TechWorld.Models;

namespace TechWorld.Repo
{
    public interface IOrderConfirmationRepo
    {
        Task SaveAsync(OrderConfirmation orderConfirmation);
        Task SaveAsync(List<ConfirmedProduct> confirmedProducts);
        Task<OrderConfirmation> GetByOrderIdAsync(int orderId);
        Task UpdateAsync(OrderConfirmation orderConfirmation);
        Task DeleteAsync(OrderConfirmation orderConfirmation);
        Task<List<OrderConfirmation>> GetAllAsync(); // Added for GET all
    }
}