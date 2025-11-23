using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gadget_CentralApi.Data;
using Gadget_CentralApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gadget_CentralApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderConfirmationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderConfirmationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/OrderConfirmation
        [HttpPost]
        public async Task<ActionResult<OrderConfirmation>> CreateOrderConfirmation([FromBody] OrderConfirmationDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var orderConfirmation = new OrderConfirmation
                {
                    OrderId = orderDto.OrderId,
                    RequestId = orderDto.RequestId,
                    ConfirmationDate = orderDto.ConfirmationDate,
                    Status = orderDto.Status,
                    UserId = orderDto.UserId,
                    ProductId = orderDto.ProductId, // This will be the distributor-specific ID (e.g., 201)
                    Stock = orderDto.Stock,
                    QuotationRequestId = orderDto.QuotationRequestId,
                    QuotationResponseId = orderDto.QuotationResponseId
                };

                _context.OrderConfirmations.Add(orderConfirmation);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Order confirmation received from GadgetHub: OrderId={orderDto.OrderId}, ProductId={orderDto.ProductId}, Status={orderDto.Status}");

                return CreatedAtAction(nameof(GetOrderConfirmation), new { id = orderConfirmation.OrderId }, orderConfirmation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating order confirmation: {ex.Message}");
                return StatusCode(500, new { message = "Error creating order confirmation", error = ex.Message });
            }
        }

        // GET: api/OrderConfirmation/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderConfirmation>> GetOrderConfirmation(int id)
        {
            try
            {
                var orderConfirmation = await _context.OrderConfirmations.FindAsync(id);

                if (orderConfirmation == null)
                {
                    return NotFound(new { message = "Order confirmation not found" });
                }

                return Ok(orderConfirmation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching order confirmation", error = ex.Message });
            }
        }

        // GET: api/OrderConfirmation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderConfirmation>>> GetOrderConfirmations()
        {
            try
            {
                var orderConfirmations = await _context.OrderConfirmations
                    .OrderByDescending(o => o.ConfirmationDate)
                    .ToListAsync();

                return Ok(orderConfirmations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching order confirmations", error = ex.Message });
            }
        }
    }

    public class OrderConfirmationDto
    {
        public int OrderId { get; set; }
        public int RequestId { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public int QuotationRequestId { get; set; }
        public int QuotationResponseId { get; set; }
    }
}