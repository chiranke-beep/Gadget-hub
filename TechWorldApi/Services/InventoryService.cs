using System;

namespace TechWorld.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly Dictionary<string, int> _inventory = new Dictionary<string, int>
          {
              { "P001", 50 }, { "P002", 30 }, { "P003", 20 }, { "P004", 40 }, { "P005", 60 }
          };

        public bool IsProductAvailable(string productId, int quantity)
        {
            if (string.IsNullOrEmpty(productId) || quantity <= 0)
                return false;

            if (_inventory.TryGetValue(productId, out int availableQuantity))
            {
                Console.WriteLine($"Checking {productId} at {DateTime.UtcNow}: Available={availableQuantity}, Requested={quantity}");
                return availableQuantity >= quantity;
            }
            return false;
        }
    }
}