using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GadgetHub.Data;
using GadgetHub.Models;
using GadgetHub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;
        private readonly IDistributorOrderService _distributorOrderService;

        public OrdersController(GadgetHubDbContext context, IDistributorOrderService distributorOrderService)
        {
            _context = context;
            _distributorOrderService = distributorOrderService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Product)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();

                var orderDtos = orders.Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    Price = o.Price,
                    ProductId = o.ProductId,
                    Stock = o.Stock,
                    QuotationRequestId = o.QuotationRequestId,
                    QuotationResponseId = o.QuotationResponseId,
                    UserName = o.User?.FullName ?? "Unknown User",
                    ProductName = o.Product?.Name ?? "Unknown Product",
                    ProductDescription = o.Product?.Description ?? "",
                    ProductImageUrl = o.Product?.Url ?? ""
                });

                return Ok(orderDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching orders", error = ex.Message });
            }
        }

        // GET: api/Orders/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUser(int userId)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Product)
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();

                var orderDtos = orders.Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    Price = o.Price,
                    ProductId = o.ProductId,
                    Stock = o.Stock,
                    QuotationRequestId = o.QuotationRequestId,
                    QuotationResponseId = o.QuotationResponseId,
                    UserName = o.User?.FullName ?? "Unknown User",
                    ProductName = o.Product?.Name ?? "Unknown Product",
                    ProductDescription = o.Product?.Description ?? "",
                    ProductImageUrl = o.Product?.Url ?? ""
                });

                return Ok(orderDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching user orders", error = ex.Message });
            }
        }

        // GET: api/Orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == id);

                if (order == null)
                {
                    return NotFound(new { message = "Order not found" });
                }

                var orderDto = new OrderDto
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    Price = order.Price,
                    ProductId = order.ProductId,
                    Stock = order.Stock,
                    QuotationRequestId = order.QuotationRequestId,
                    QuotationResponseId = order.QuotationResponseId,
                    UserName = order.User?.FullName ?? "Unknown User",
                    ProductName = order.Product?.Name ?? "Unknown Product",
                    ProductDescription = order.Product?.Description ?? "",
                    ProductImageUrl = order.Product?.Url ?? ""
                };

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching order", error = ex.Message });
            }
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Console.WriteLine($"Creating order with status: {createOrderDto.Status}");

                var order = new Order
                {
                    UserId = createOrderDto.UserId,
                    OrderDate = DateTime.UtcNow,
                    Status = createOrderDto.Status ?? "processing", // Use the status from request
                    Price = createOrderDto.Price,
                    ProductId = createOrderDto.ProductId,
                    Stock = createOrderDto.Stock,
                    QuotationRequestId = createOrderDto.QuotationRequestId,
                    QuotationResponseId = createOrderDto.QuotationResponseId
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Order created with ID: {order.OrderId}, Status: {order.Status}");

                // Send order to distributors if status is "Confirmed"
                if (order.Status.ToLower() == "confirmed")
                {
                    Console.WriteLine($"Order {order.OrderId} is confirmed, sending to distributors...");
                    Console.WriteLine($"DistributorOrderService is null: {_distributorOrderService == null}");
                    var result = await _distributorOrderService.SendOrderToDistributorsAsync(order);
                    Console.WriteLine($"Distributor service result: {result}");
                }
                else
                {
                    Console.WriteLine($"Order {order.OrderId} is not confirmed (Status: {order.Status}), skipping distributor notification");
                }

                // Fetch the created order with related data
                var createdOrder = await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

                if (createdOrder == null)
                {
                    return StatusCode(500, new { message = "Error retrieving created order" });
                }

                var orderDto = new OrderDto
                {
                    OrderId = createdOrder.OrderId,
                    UserId = createdOrder.UserId,
                    OrderDate = createdOrder.OrderDate,
                    Status = createdOrder.Status,
                    Price = createdOrder.Price,
                    ProductId = createdOrder.ProductId,
                    Stock = createdOrder.Stock,
                    QuotationRequestId = createdOrder.QuotationRequestId,
                    QuotationResponseId = createdOrder.QuotationResponseId,
                    UserName = createdOrder.User?.FullName ?? "Unknown User",
                    ProductName = createdOrder.Product?.Name ?? "Unknown Product",
                    ProductDescription = createdOrder.Product?.Description ?? "",
                    ProductImageUrl = createdOrder.Product?.Url ?? ""
                };

                return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, orderDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating order: {ex.Message}");
                return StatusCode(500, new { message = "Error creating order", error = ex.Message });
            }
        }

        // PUT: api/Orders/{id}/status
        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto updateDto)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound(new { message = "Order not found" });
                }

                order.Status = updateDto.Status;
                await _context.SaveChangesAsync();

                // Send order to distributors if status is "Confirmed"
                if (updateDto.Status.ToLower() == "confirmed")
                {
                    await _distributorOrderService.SendOrderToDistributorsAsync(order);
                }

                return Ok(new { message = "Order status updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating order status", error = ex.Message });
            }
        }

        // GET: api/Orders/test-distributor
        [HttpGet("test-distributor")]
        public async Task<ActionResult> TestDistributorService()
        {
            try
            {
                var testOrder = new Order
                {
                    // Don't set OrderId - let database generate it
                    UserId = 1,
                    OrderDate = DateTime.UtcNow,
                    Status = "Confirmed",
                    Price = 100.00m,
                    ProductId = 1,
                    Stock = 10,
                    QuotationRequestId = 1,
                    QuotationResponseId = 1
                };

                Console.WriteLine("Testing DistributorOrderService...");
                var result = await _distributorOrderService.SendOrderToDistributorsAsync(testOrder);
                Console.WriteLine($"Test result: {result}");

                return Ok(new { message = "DistributorOrderService test completed", result = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test error: {ex.Message}");
                return StatusCode(500, new { message = "Test failed", error = ex.Message });
            }
        }

        // GET: api/Orders/test-mapping
        [HttpGet("test-mapping")]
        public async Task<ActionResult> TestProductMapping()
        {
            try
            {
                var mapping = await _context.ProductDistributorMaps
                    .FirstOrDefaultAsync(m => m.GadgetHubId == 1);

                if (mapping == null)
                {
                    return Ok(new { message = "No product mapping found for ProductId 1" });
                }

                return Ok(new { 
                    message = "Product mapping found", 
                    mapping = new { 
                        GadgetHubId = mapping.GadgetHubId,
                        ElectroComId = mapping.ElectroComId,
                        TechWorldId = mapping.TechWorldId,
                        GadgetCentralId = mapping.GadgetCentralId
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Test failed", error = ex.Message });
            }
        }
    }

    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public int QuotationRequestId { get; set; }
        public int QuotationResponseId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductImageUrl { get; set; } = string.Empty;
    }

    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public int QuotationRequestId { get; set; }
        public int QuotationResponseId { get; set; }
    }

    public class UpdateOrderStatusDto
    {
        public string Status { get; set; } = string.Empty;
    }
} 