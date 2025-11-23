using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gadget_CentralApi.Models;
using Gadget_CentralApi.DTO;
using Gadget_CentralApi.Data;
using System;
using System.Linq;
using AutoMapper;
using Gadget_CentralApi.Services;

namespace Gadget_CentralApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDistributorMappingService _distributorMappingService;
        
        public QuotationsController(ApplicationDbContext context, IMapper mapper, IDistributorMappingService distributorMappingService) 
        { 
            _context = context; 
            _mapper = mapper;
            _distributorMappingService = distributorMappingService;
        }

        [HttpPost]
        public async Task<ActionResult<QuotationResponseDto>> CreateQuotationRequest([FromBody] List<ProductRequestDto> productRequests)
        {
            if (productRequests == null || !productRequests.Any())
                return BadRequest("Product requests cannot be null or empty.");

            var quotationRequest = new QuotationRequest
            {
                RequestDate = DateTime.UtcNow,
                ProductRequestInfos = new List<ProductRequestInfo>()
            };

            _context.QuotationRequests.Add(quotationRequest);
            await _context.SaveChangesAsync();

            // Set RequestId to the auto-generated Id
            quotationRequest.RequestId = quotationRequest.Id;
            await _context.SaveChangesAsync();

            var productQuoteInfos = new List<ProductQuoteInfoReadDto>();
            var random = new Random();

            foreach (var req in productRequests)
            {
                // Get the mapped distributor ID for this product
                var mappedProductId = await _distributorMappingService.GetDistributorIdAsync(req.ProductId, "gadgetcentral");

                var productRequestInfo = _mapper.Map<ProductRequestInfo>(req);
                productRequestInfo.QuotationRequestId = quotationRequest.Id;
                productRequestInfo.ProductId = mappedProductId; // Use mapped distributor ID instead of original productId
                _context.ProductRequestInfos.Add(productRequestInfo);

                // Simulate quote info with mapped distributor ID
                productQuoteInfos.Add(new ProductQuoteInfoReadDto
                {
                    ProductId = mappedProductId, // Use mapped distributor ID instead of original productId
                    PricePerUnit = 100.00m + random.Next(10, 50),
                    AvailableQuantity = req.Quantity + random.Next(0, 10),
                    EstimatedDeliveryDays = random.Next(2, 7)
                });
            }
            await _context.SaveChangesAsync();

            // Get the correct distributor ID from ProductDistributorMaps
            var distributorId = await _distributorMappingService.GetDistributorIdAsync(productRequests.First().ProductId, "gadgetcentral");

            var response = new QuotationResponseDto
            {
                RequestId = quotationRequest.RequestId, // int
                ResponseDate = DateTime.UtcNow,
                DistributerId = distributorId, // Use mapped distributor ID
                ProductQuotes = productQuoteInfos
            };
            return Ok(response);
        }
    }
}
