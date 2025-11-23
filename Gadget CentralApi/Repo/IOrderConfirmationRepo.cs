using Gadget_CentralApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gadget_CentralApi.Repo
{
    public interface IOrderConfirmationRepo
    {
        Task SaveAsync(OrderConfirmation orderConfirmation);
        Task SaveAsync(List<ConfirmedProduct> confirmedProducts);
        Task<OrderConfirmation> GetByOrderIdAsync(int orderId);
        Task UpdateAsync(OrderConfirmation orderConfirmation);
        Task DeleteAsync(OrderConfirmation orderConfirmation);
        Task<List<OrderConfirmation>> GetAllAsync();
    }
}