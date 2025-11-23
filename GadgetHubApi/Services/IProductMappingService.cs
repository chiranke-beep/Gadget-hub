using System.Collections.Generic;
using System.Threading.Tasks;
using GadgetHub.DTOs;

namespace GadgetHub.Services
{
    public interface IProductMappingService
    {
        Task<List<ProductRequestDto>> MapToDistributorIds(List<ProductRequestDto> productRequests, string distributorType);
        Task<List<ProductRequestDto>> MapFromDistributorIds(List<ProductRequestDto> productRequests, string distributorType);
    }
} 