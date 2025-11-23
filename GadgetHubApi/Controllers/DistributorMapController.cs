using Microsoft.AspNetCore.Mvc;
using GadgetHub.Models;
using GadgetHub.Data;
using GadgetHub.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace GadgetHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistributorMapController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;
        
        public DistributorMapController(GadgetHubDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDistributorMapDto>>> GetMaps()
        {
            var maps = await _context.ProductDistributorMaps
                .Include(m => m.Product)
                .Select(m => new ProductDistributorMapDto
                {
                    Id = m.Id,
                    GadgetHubId = m.GadgetHubId,
                    TechWorldId = m.TechWorldId,
                    GadgetCentralId = m.GadgetCentralId,
                    ElectroComId = m.ElectroComId,
                    ProductName = m.Product.Name,
                    ProductDescription = m.Product.Description
                })
                .ToListAsync();
            return Ok(maps);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDistributorMapDto>> GetMap(int id)
        {
            var map = await _context.ProductDistributorMaps
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (map == null) return NotFound();
            
            var dto = new ProductDistributorMapDto
            {
                Id = map.Id,
                GadgetHubId = map.GadgetHubId,
                TechWorldId = map.TechWorldId,
                GadgetCentralId = map.GadgetCentralId,
                ElectroComId = map.ElectroComId,
                ProductName = map.Product.Name,
                ProductDescription = map.Product.Description
            };
            return Ok(dto);
        }

        [HttpGet("product/{gadgetHubId}")]
        public async Task<ActionResult<ProductDistributorMapDto>> GetMapByGadgetHubId(int gadgetHubId)
        {
            var map = await _context.ProductDistributorMaps
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.GadgetHubId == gadgetHubId);
            if (map == null) return NotFound();
            
            var dto = new ProductDistributorMapDto
            {
                Id = map.Id,
                GadgetHubId = map.GadgetHubId,
                TechWorldId = map.TechWorldId,
                GadgetCentralId = map.GadgetCentralId,
                ElectroComId = map.ElectroComId,
                ProductName = map.Product.Name,
                ProductDescription = map.Product.Description
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDistributorMapDto>> CreateMap(ProductDistributorMapWriteDto dto)
        {
            // Check if product exists
            var product = await _context.Products.FindAsync(dto.GadgetHubId);
            if (product == null) return BadRequest("Product not found");
            
            // Check if mapping already exists for this product
            var existingMap = await _context.ProductDistributorMaps
                .FirstOrDefaultAsync(m => m.GadgetHubId == dto.GadgetHubId);
            if (existingMap != null) return BadRequest("Mapping already exists for this product");
            
            var map = new ProductDistributorMap
            {
                GadgetHubId = dto.GadgetHubId,
                TechWorldId = dto.TechWorldId,
                GadgetCentralId = dto.GadgetCentralId,
                ElectroComId = dto.ElectroComId
            };
            
            _context.ProductDistributorMaps.Add(map);
            await _context.SaveChangesAsync();
            
            var resultDto = new ProductDistributorMapDto
            {
                Id = map.Id,
                GadgetHubId = map.GadgetHubId,
                TechWorldId = map.TechWorldId,
                GadgetCentralId = map.GadgetCentralId,
                ElectroComId = map.ElectroComId,
                ProductName = product.Name,
                ProductDescription = product.Description
            };
            
            return CreatedAtAction(nameof(GetMap), new { id = map.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMap(int id, ProductDistributorMapWriteDto dto)
        {
            var map = await _context.ProductDistributorMaps.FindAsync(id);
            if (map == null) return NotFound();
            
            // Check if product exists
            var product = await _context.Products.FindAsync(dto.GadgetHubId);
            if (product == null) return BadRequest("Product not found");
            
            map.GadgetHubId = dto.GadgetHubId;
            map.TechWorldId = dto.TechWorldId;
            map.GadgetCentralId = dto.GadgetCentralId;
            map.ElectroComId = dto.ElectroComId;
            
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMap(int id)
        {
            var map = await _context.ProductDistributorMaps.FindAsync(id);
            if (map == null) return NotFound();
            _context.ProductDistributorMaps.Remove(map);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
