using Microsoft.AspNetCore.Mvc;
using GadgetHub.Models;
using GadgetHub.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace GadgetHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinalResponseController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;
        
        public FinalResponseController(GadgetHubDbContext context) 
        { 
            _context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinalResponse>>> GetFinalResponses() 
        {
            try
            {
                var responses = await _context.FinalResponses
                    .OrderByDescending(fr => fr.Id)
                    .ToListAsync();
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve final responses", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinalResponse>> GetFinalResponse(int id)
        {
            try
            {
                var final = await _context.FinalResponses.FindAsync(id);
                if (final == null) 
                    return NotFound(new { error = "Final response not found" });
                
                return Ok(final);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve final response", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<FinalResponse>> CreateFinalResponse(FinalResponse final)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { error = "Invalid model state", details = ModelState });
                }

                _context.FinalResponses.Add(final);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetFinalResponse), new { id = final.Id }, final);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to create final response", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFinalResponse(int id, FinalResponse final)
        {
            try
            {
                if (id != final.Id) 
                    return BadRequest(new { error = "ID mismatch" });

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { error = "Invalid model state", details = ModelState });
                }

                _context.Entry(final).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.FinalResponses.AnyAsync(fr => fr.Id == id);
                if (!exists)
                {
                    return NotFound(new { error = "Final response not found" });
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to update final response", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinalResponse(int id)
        {
            try
            {
                var final = await _context.FinalResponses.FindAsync(id);
                if (final == null) 
                    return NotFound(new { error = "Final response not found" });
                
                _context.FinalResponses.Remove(final);
                await _context.SaveChangesAsync();
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to delete final response", details = ex.Message });
            }
        }

        [HttpGet("by-quotation-request/{quotationRequestId}")]
        public async Task<ActionResult<IEnumerable<FinalResponse>>> GetFinalResponsesByQuotationRequest(int quotationRequestId)
        {
            try
            {
                var responses = await _context.FinalResponses
                    .Where(fr => fr.QuotationRequestId == quotationRequestId)
                    .OrderBy(fr => fr.Price)
                    .ToListAsync();
                
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve final responses by quotation request", details = ex.Message });
            }
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<FinalResponse>>> GetFinalResponsesByUser(int userId)
        {
            try
            {
                var responses = await _context.FinalResponses
                    .Where(fr => fr.UserId == userId)
                    .OrderByDescending(fr => fr.Id)
                    .ToListAsync();
                
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve final responses by user", details = ex.Message });
            }
        }

        [HttpGet("by-product/{productId}")]
        public async Task<ActionResult<IEnumerable<FinalResponse>>> GetFinalResponsesByProduct(int productId)
        {
            try
            {
                var responses = await _context.FinalResponses
                    .Where(fr => fr.ProductId == productId)
                    .OrderByDescending(fr => fr.Id)
                    .ToListAsync();
                
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve final responses by product", details = ex.Message });
            }
        }

        [HttpDelete("clear-by-quotation-request/{quotationRequestId}")]
        public async Task<IActionResult> ClearFinalResponsesByQuotationRequest(int quotationRequestId)
        {
            try
            {
                var responsesToDelete = await _context.FinalResponses
                    .Where(fr => fr.QuotationRequestId == quotationRequestId)
                    .ToListAsync();

                if (!responsesToDelete.Any())
                {
                    return NotFound(new { error = "No final responses found for this quotation request" });
                }

                _context.FinalResponses.RemoveRange(responsesToDelete);
                await _context.SaveChangesAsync();
                
                return Ok(new { message = $"Cleared {responsesToDelete.Count} final responses for quotation request {quotationRequestId}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to clear final responses", details = ex.Message });
            }
        }

        [HttpGet("best-quotation/{quotationRequestId}")]
        public async Task<ActionResult<FinalResponse>> GetBestQuotation(int quotationRequestId)
        {
            try
            {
                var responses = await _context.FinalResponses
                    .Where(fr => fr.QuotationRequestId == quotationRequestId)
                    .ToListAsync();

                if (!responses.Any())
                {
                    return NotFound(new { error = "No responses found for this quotation request" });
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

                return Ok(bestResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to get best quotation", details = ex.Message });
            }
        }
    }
}
