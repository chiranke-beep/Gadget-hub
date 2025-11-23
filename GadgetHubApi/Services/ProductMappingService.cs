using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetHub.Data;
using GadgetHub.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.Services
{
    public class ProductMappingService : IProductMappingService
    {
        private readonly GadgetHubDbContext _context;

        public ProductMappingService(GadgetHubDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductRequestDto>> MapToDistributorIds(List<ProductRequestDto> productRequests, string distributorType)
        {
            var mappedRequests = new List<ProductRequestDto>();

            foreach (var request in productRequests)
            {
                if (int.TryParse(request.ProductId, out int gadgetHubId))
                {
                    var distributorMap = await _context.ProductDistributorMaps
                        .FirstOrDefaultAsync(pdm => pdm.GadgetHubId == gadgetHubId);

                    if (distributorMap != null)
                    {
                        string? distributorId = distributorType.ToLower() switch
                        {
                            "techworld" => distributorMap.TechWorldId,
                            "gadgetcentral" => distributorMap.GadgetCentralId,
                            "electrocom" => distributorMap.ElectroComId,
                            _ => null
                        };

                        if (!string.IsNullOrEmpty(distributorId))
                        {
                            mappedRequests.Add(new ProductRequestDto
                            {
                                ProductId = distributorId,
                                Quantity = request.Quantity,
                                UserId = request.UserId,
                                RequestedBy = request.RequestedBy
                            });
                        }
                    }
                }
            }

            return mappedRequests;
        }

        public async Task<List<ProductRequestDto>> MapFromDistributorIds(List<ProductRequestDto> productRequests, string distributorType)
        {
            var mappedRequests = new List<ProductRequestDto>();

            foreach (var request in productRequests)
            {
                var distributorMap = distributorType.ToLower() switch
                {
                    "techworld" => await _context.ProductDistributorMaps
                        .FirstOrDefaultAsync(pdm => pdm.TechWorldId == request.ProductId),
                    "gadgetcentral" => await _context.ProductDistributorMaps
                        .FirstOrDefaultAsync(pdm => pdm.GadgetCentralId == request.ProductId),
                    "electrocom" => await _context.ProductDistributorMaps
                        .FirstOrDefaultAsync(pdm => pdm.ElectroComId == request.ProductId),
                    _ => null
                };

                if (distributorMap != null)
                {
                    mappedRequests.Add(new ProductRequestDto
                    {
                        ProductId = distributorMap.GadgetHubId.ToString(),
                        Quantity = request.Quantity,
                        UserId = request.UserId,
                        RequestedBy = request.RequestedBy
                    });
                }
            }

            return mappedRequests;
        }
    }
} 