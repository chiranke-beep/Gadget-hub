using GadgetHub.Models;

namespace GadgetHub.Services
{
    public interface IDistributorOrderService
    {
        Task<bool> SendOrderToDistributorsAsync(Order order);
    }
} 