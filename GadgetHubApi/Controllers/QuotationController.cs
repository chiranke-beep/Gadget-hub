using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GadgetHub.Data;
using GadgetHub.Models;
using GadgetHub.Services;

namespace GadgetHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;
        private readonly IQuotationComparisonService _comparisonService;

        public QuotationController(GadgetHubDbContext context, IQuotationComparisonService comparisonService)
        {
            _context = context;
            _comparisonService = comparisonService;
        }

        [HttpPost("store-response")]
        public async Task<IActionResult> StoreDistributorResponse([FromBody] DistributorResponseDto response)
        {
            try
            {
                Console.WriteLine($"Received response: UserId={response.UserId}, ProductId={response.ProductId}, Price={response.Price}, Stock={response.Stock}");
                
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(new { error = "Model validation failed", details = errors });
                }
                
                // Store the distributor response
                await _comparisonService.StoreDistributorResponseAsync(response);
                
                // Check if we have responses from all 3 distributors for this quotation request
                var responsesForRequest = await _context.FinalResponses
                    .Where(r => r.QuotationRequestId == response.QuotationRequestId)
                    .ToListAsync();
                
                Console.WriteLine($"Found {responsesForRequest.Count} responses for request {response.QuotationRequestId}");
                
                // If we have 3 or more responses, automatically compare and store the best one
                if (responsesForRequest.Count >= 3)
                {
                    Console.WriteLine($"Triggering automatic comparison for request {response.QuotationRequestId}");
                    var bestQuotation = await _comparisonService.CompareAndStoreBestQuotationAsync(response.QuotationRequestId);
                    await _comparisonService.ClearFinalResponsesAsync(response.QuotationRequestId);
                    
                    return Ok(new { 
                        message = "Distributor response stored and comparison completed automatically",
                        bestQuotation = new
                        {
                            bestQuotation.Id,
                            bestQuotation.UserId,
                            bestQuotation.ProductId,
                            bestQuotation.Price,
                            bestQuotation.Stock,
                            bestQuotation.Status
                        }
                    });
                }
                else
                {
                    return Ok(new { 
                        message = $"Distributor response stored successfully. Waiting for more responses. ({responsesForRequest.Count}/3 received)",
                        responsesReceived = responsesForRequest.Count
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error storing distributor response: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("compare/{quotationRequestId}")]
        public async Task<IActionResult> CompareQuotations(int quotationRequestId)
        {
            try
            {
                // Compare quotations and store the best one
                var bestQuotation = await _comparisonService.CompareAndStoreBestQuotationAsync(quotationRequestId);
                
                // Clear the FinalResponses table
                await _comparisonService.ClearFinalResponsesAsync(quotationRequestId);

                return Ok(new { 
                    message = "Quotations compared and best one stored",
                    bestQuotation = new
                    {
                        bestQuotation.Id,
                        bestQuotation.UserId,
                        bestQuotation.ProductId,
                        bestQuotation.Price,
                        bestQuotation.Stock,
                        bestQuotation.Status
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("final-responses/{quotationRequestId}")]
        public async Task<IActionResult> GetFinalResponses(int quotationRequestId)
        {
            try
            {
                var responses = await _context.FinalResponses
                    .Where(r => r.QuotationRequestId == quotationRequestId)
                    .ToListAsync();

                return Ok(responses);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("best-quotations")]
        public async Task<IActionResult> GetBestQuotations()
        {
            try
            {
                var bestQuotations = await _context.FullyComparedResponses
                    .OrderByDescending(q => q.Id)
                    .ToListAsync();

                return Ok(bestQuotations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/Quotation/remove-quotation/{id}
        [HttpDelete("remove-quotation/{id}")]
        public async Task<ActionResult> RemoveQuotation(int id)
        {
            try
            {
                var quotation = await _context.FullyComparedResponses.FindAsync(id);
                if (quotation == null)
                {
                    return NotFound(new { message = "Quotation not found" });
                }

                _context.FullyComparedResponses.Remove(quotation);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Quotation removed successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error removing quotation", error = ex.Message });
            }
        }

        [HttpPost("process-quotation")]
        public async Task<IActionResult> ProcessQuotation([FromBody] QuotationRequestDto request)
        {
            try
            {
                // Store responses from all distributors
                foreach (var distributorResponse in request.DistributorResponses)
                {
                    await _comparisonService.StoreDistributorResponseAsync(distributorResponse);
                }

                // Compare and store the best quotation
                var bestQuotation = await _comparisonService.CompareAndStoreBestQuotationAsync(request.QuotationRequestId);
                
                // Clear the FinalResponses table
                await _comparisonService.ClearFinalResponsesAsync(request.QuotationRequestId);

                return Ok(new { 
                    message = "Quotation processing completed",
                    bestQuotation = new
                    {
                        bestQuotation.Id,
                        bestQuotation.UserId,
                        bestQuotation.ProductId,
                        bestQuotation.Price,
                        bestQuotation.Stock,
                        bestQuotation.Status
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    public class QuotationRequestDto
    {
        public int QuotationRequestId { get; set; }
        public List<DistributorResponseDto> DistributorResponses { get; set; } = new List<DistributorResponseDto>();
    }
}
