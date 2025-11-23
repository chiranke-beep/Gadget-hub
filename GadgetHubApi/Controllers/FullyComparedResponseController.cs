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
    public class FullyComparedResponseController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;
        
        public FullyComparedResponseController(GadgetHubDbContext context) 
        { 
            _context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FullyComparedResponse>>> GetFullyComparedResponses() 
        {
            try
            {
                var responses = await _context.FullyComparedResponses
                    .OrderByDescending(fcr => fcr.Id)
                    .ToListAsync();
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve fully compared responses", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FullyComparedResponse>> GetFullyComparedResponse(int id)
        {
            try
            {
                var response = await _context.FullyComparedResponses.FindAsync(id);
                if (response == null) 
                    return NotFound(new { error = "Fully compared response not found" });
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve fully compared response", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<FullyComparedResponse>> CreateFullyComparedResponse(FullyComparedResponse response)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { error = "Invalid model state", details = ModelState });
                }

                _context.FullyComparedResponses.Add(response);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetFullyComparedResponse), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to create fully compared response", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFullyComparedResponse(int id, FullyComparedResponse response)
        {
            try
            {
                if (id != response.Id) 
                    return BadRequest(new { error = "ID mismatch" });

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { error = "Invalid model state", details = ModelState });
                }

                _context.Entry(response).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.FullyComparedResponses.AnyAsync(fcr => fcr.Id == id);
                if (!exists)
                {
                    return NotFound(new { error = "Fully compared response not found" });
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to update fully compared response", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFullyComparedResponse(int id)
        {
            try
            {
                var response = await _context.FullyComparedResponses.FindAsync(id);
                if (response == null) 
                    return NotFound(new { error = "Fully compared response not found" });
                
                _context.FullyComparedResponses.Remove(response);
                await _context.SaveChangesAsync();
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to delete fully compared response", details = ex.Message });
            }
        }

        [HttpGet("by-quotation-request/{quotationRequestId}")]
        public async Task<ActionResult<FullyComparedResponse>> GetFullyComparedResponseByQuotationRequest(int quotationRequestId)
        {
            try
            {
                var response = await _context.FullyComparedResponses
                    .Where(fcr => fcr.QuotationRequestId == quotationRequestId)
                    .FirstOrDefaultAsync();
                
                if (response == null)
                {
                    return NotFound(new { error = "No fully compared response found for this quotation request" });
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve fully compared response by quotation request", details = ex.Message });
            }
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<FullyComparedResponse>>> GetFullyComparedResponsesByUser(int userId)
        {
            try
            {
                var responses = await _context.FullyComparedResponses
                    .Where(fcr => fcr.UserId == userId)
                    .OrderByDescending(fcr => fcr.Id)
                    .ToListAsync();
                
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve fully compared responses by user", details = ex.Message });
            }
        }

        [HttpGet("by-product/{productId}")]
        public async Task<ActionResult<IEnumerable<FullyComparedResponse>>> GetFullyComparedResponsesByProduct(int productId)
        {
            try
            {
                var responses = await _context.FullyComparedResponses
                    .Where(fcr => fcr.ProductId == productId)
                    .OrderByDescending(fcr => fcr.Id)
                    .ToListAsync();
                
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve fully compared responses by product", details = ex.Message });
            }
        }

        [HttpGet("recent/{count}")]
        public async Task<ActionResult<IEnumerable<FullyComparedResponse>>> GetRecentFullyComparedResponses(int count = 10)
        {
            try
            {
                if (count <= 0 || count > 100)
                {
                    return BadRequest(new { error = "Count must be between 1 and 100" });
                }

                var responses = await _context.FullyComparedResponses
                    .OrderByDescending(fcr => fcr.Id)
                    .Take(count)
                    .ToListAsync();
                
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve recent fully compared responses", details = ex.Message });
            }
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<object>> GetFullyComparedResponseStatistics()
        {
            try
            {
                var totalCount = await _context.FullyComparedResponses.CountAsync();
                var averagePrice = await _context.FullyComparedResponses.AverageAsync(fcr => fcr.Price);
                var totalStock = await _context.FullyComparedResponses.SumAsync(fcr => fcr.Stock);
                var uniqueUsers = await _context.FullyComparedResponses.Select(fcr => fcr.UserId).Distinct().CountAsync();
                var uniqueProducts = await _context.FullyComparedResponses.Select(fcr => fcr.ProductId).Distinct().CountAsync();

                return Ok(new
                {
                    totalCount,
                    averagePrice = Math.Round(averagePrice, 2),
                    totalStock,
                    uniqueUsers,
                    uniqueProducts
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve statistics", details = ex.Message });
            }
        }

        [HttpDelete("clear-all")]
        public async Task<IActionResult> ClearAllFullyComparedResponses()
        {
            try
            {
                var count = await _context.FullyComparedResponses.CountAsync();
                if (count == 0)
                {
                    return NotFound(new { error = "No fully compared responses to clear" });
                }

                _context.FullyComparedResponses.RemoveRange(_context.FullyComparedResponses);
                await _context.SaveChangesAsync();
                
                return Ok(new { message = $"Cleared {count} fully compared responses" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to clear all fully compared responses", details = ex.Message });
            }
        }
    }
}
