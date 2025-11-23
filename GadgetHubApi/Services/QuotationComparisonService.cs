using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GadgetHub.Data;
using GadgetHub.Models;

namespace GadgetHub.Services
{
    public interface IQuotationComparisonService
    {
        Task StoreDistributorResponseAsync(DistributorResponseDto response);
        Task<FullyComparedResponse> CompareAndStoreBestQuotationAsync(int quotationRequestId);
        Task ClearFinalResponsesAsync(int quotationRequestId);
    }

    public class QuotationComparisonService : IQuotationComparisonService
    {
        private readonly GadgetHubDbContext _context;
        private readonly IReverseMappingService _reverseMappingService;

        public QuotationComparisonService(GadgetHubDbContext context, IReverseMappingService reverseMappingService)
        {
            _context = context;
            _reverseMappingService = reverseMappingService;
        }

        public async Task StoreDistributorResponseAsync(DistributorResponseDto response)
        {
            // Convert distributor ProductId back to GadgetHub ProductId
            var gadgetHubProductId = await _reverseMappingService.GetGadgetHubProductIdAsync(response.ProductId);

            var finalResponse = new FinalResponse
            {
                UserId = response.UserId,
                ProductId = gadgetHubProductId, // Use converted GadgetHub ProductId
                Price = response.Price,
                Stock = response.Stock,
                QuotationRequestId = response.QuotationRequestId,
                QuotationResponseId = response.QuotationResponseId,
                Status = response.Status
            };

            _context.FinalResponses.Add(finalResponse);
            await _context.SaveChangesAsync();
        }

        public async Task<FullyComparedResponse> CompareAndStoreBestQuotationAsync(int quotationRequestId)
        {
            // Get all responses for this quotation request
            var responses = await _context.FinalResponses
                .Where(r => r.QuotationRequestId == quotationRequestId)
                .ToListAsync();

            if (!responses.Any())
            {
                throw new InvalidOperationException($"No responses found for quotation request {quotationRequestId}");
            }

            // Find the best quotation (lowest price with available stock)
            var bestResponse = responses
                .Where(r => r.Stock > 0) // Only consider responses with available stock
                .OrderBy(r => r.Price) // Order by price (lowest first)
                .ThenByDescending(r => r.Stock) // Then by stock (highest first)
                .FirstOrDefault();

            // If no response has stock, take the lowest price
            if (bestResponse == null)
            {
                bestResponse = responses.OrderBy(r => r.Price).First();
            }

            // Store the best response in FullyComparedResponses
            var fullyComparedResponse = new FullyComparedResponse
            {
                UserId = bestResponse.UserId,
                ProductId = bestResponse.ProductId, // Already converted to GadgetHub ProductId
                Price = bestResponse.Price,
                Stock = bestResponse.Stock,
                QuotationRequestId = bestResponse.QuotationRequestId,
                QuotationResponseId = bestResponse.QuotationResponseId,
                Status = "Best Quotation Selected"
            };

            _context.FullyComparedResponses.Add(fullyComparedResponse);
            await _context.SaveChangesAsync();

            return fullyComparedResponse;
        }

        public async Task ClearFinalResponsesAsync(int quotationRequestId)
        {
            var responsesToDelete = await _context.FinalResponses
                .Where(r => r.QuotationRequestId == quotationRequestId)
                .ToListAsync();

            _context.FinalResponses.RemoveRange(responsesToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public class DistributorResponseDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int QuotationRequestId { get; set; }
        public int QuotationResponseId { get; set; }
        public string Status { get; set; }
    }
} 