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
    public class DistributorResponseController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;
        
        public DistributorResponseController(GadgetHubDbContext context) 
        { 
            _context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistributorResponse>>> GetDistributorResponses() 
        {
            try
            {
                var responses = await _context.DistributorResponses
                    .Include(dr => dr.User)
                    .Include(dr => dr.Product)
                    .ToListAsync();
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve distributor responses", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DistributorResponse>> GetDistributorResponse(int id)
        {
            try
            {
                var distributorResponse = await _context.DistributorResponses
                    .Include(dr => dr.User)
                    .Include(dr => dr.Product)
                    .FirstOrDefaultAsync(dr => dr.Id == id);
                
                if (distributorResponse == null) 
                    return NotFound(new { error = "Distributor response not found" });
                
                return Ok(distributorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve distributor response", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<DistributorResponse>> CreateDistributorResponse(DistributorResponse distributorResponse)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { error = "Invalid model state", details = ModelState });
                }

                _context.DistributorResponses.Add(distributorResponse);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(
                    nameof(GetDistributorResponse), 
                    new { id = distributorResponse.Id }, 
                    distributorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to create distributor response", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistributorResponse(int id, DistributorResponse distributorResponse)
        {
            try
            {
                if (id != distributorResponse.Id) 
                    return BadRequest(new { error = "ID mismatch" });

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { error = "Invalid model state", details = ModelState });
                }

                _context.Entry(distributorResponse).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.DistributorResponses.AnyAsync(dr => dr.Id == id);
                if (!exists)
                {
                    return NotFound(new { error = "Distributor response not found" });
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to update distributor response", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistributorResponse(int id)
        {
            try
            {
                var distributorResponse = await _context.DistributorResponses.FindAsync(id);
                if (distributorResponse == null) 
                    return NotFound(new { error = "Distributor response not found" });
                
                _context.DistributorResponses.Remove(distributorResponse);
                await _context.SaveChangesAsync();
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to delete distributor response", details = ex.Message });
            }
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<DistributorResponse>>> GetDistributorResponsesByUser(int userId)
        {
            try
            {
                var responses = await _context.DistributorResponses
                    .Include(dr => dr.User)
                    .Include(dr => dr.Product)
                    .Where(dr => dr.UserId == userId)
                    .ToListAsync();
                
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve user distributor responses", details = ex.Message });
            }
        }

        [HttpGet("by-product/{productId}")]
        public async Task<ActionResult<IEnumerable<DistributorResponse>>> GetDistributorResponsesByProduct(int productId)
        {
            try
            {
                var responses = await _context.DistributorResponses
                    .Include(dr => dr.User)
                    .Include(dr => dr.Product)
                    .Where(dr => dr.ProductId == productId)
                    .ToListAsync();
                
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve product distributor responses", details = ex.Message });
            }
        }
    }
} 