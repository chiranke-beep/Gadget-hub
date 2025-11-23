using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GadgetHub.Data;
using GadgetHub.Models;

namespace GadgetHub.Services
{
    public interface IReverseMappingService
    {
        Task<int> GetGadgetHubProductIdAsync(int distributorProductId);
    }

    public class ReverseMappingService : IReverseMappingService
    {
        private readonly GadgetHubDbContext _context;

        public ReverseMappingService(GadgetHubDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetGadgetHubProductIdAsync(int distributorProductId)
        {
            // Convert the integer distributor ID to string for comparison
            var distributorProductIdString = distributorProductId.ToString();

            // Find the mapping where the distributor-specific ID matches
            var mapping = await _context.ProductDistributorMaps
                .FirstOrDefaultAsync(m => 
                    m.ElectroComId == distributorProductIdString || 
                    m.TechWorldId == distributorProductIdString || 
                    m.GadgetCentralId == distributorProductIdString);

            if (mapping != null)
            {
                return mapping.GadgetHubId;
            }

            // If no mapping found, return the original ID
            return distributorProductId;
        }
    }
} 