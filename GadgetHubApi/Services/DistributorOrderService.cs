using GadgetHub.Models;
using GadgetHub.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace GadgetHub.Services
{
    public class DistributorOrderService : IDistributorOrderService
    {
        private readonly GadgetHubDbContext _context;
        private readonly HttpClient _httpClient;

        public DistributorOrderService(GadgetHubDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<bool> SendOrderToDistributorsAsync(Order order)
        {
            try
            {
                Console.WriteLine($"DistributorOrderService: Starting to send order {order.OrderId} to distributors");
                
                // Only send confirmed orders
                if (order.Status.ToLower() != "confirmed")
                {
                    Console.WriteLine($"Order {order.OrderId} is not confirmed (Status: {order.Status}), skipping distributor notification");
                    return true;
                }

                Console.WriteLine($"Order {order.OrderId} is confirmed, proceeding with distributor notification");

                // Get the product mapping to convert GadgetHub ProductId to distributor-specific IDs
                var productMapping = await _context.ProductDistributorMaps
                    .FirstOrDefaultAsync(m => m.GadgetHubId == order.ProductId);

                if (productMapping == null)
                {
                    Console.WriteLine($"No product mapping found for GadgetHub ProductId: {order.ProductId}");
                    return false;
                }

                Console.WriteLine($"Found product mapping for ProductId {order.ProductId}: ElectroCom={productMapping.ElectroComId}, TechWorld={productMapping.TechWorldId}, GadgetCentral={productMapping.GadgetCentralId}");

                var tasks = new List<Task<bool>>();

                // Send to ElectroCom (Port 5258)
                if (!string.IsNullOrEmpty(productMapping.ElectroComId))
                {
                    Console.WriteLine($"Sending order to ElectroCom with ProductId: {productMapping.ElectroComId}");
                    tasks.Add(SendOrderToDistributorAsync(
                        "http://localhost:5258/api/OrderConfirmation",
                        order,
                        int.Parse(productMapping.ElectroComId),
                        "ElectroCom"
                    ));
                }

                // Send to TechWorld (Port 5213)
                if (!string.IsNullOrEmpty(productMapping.TechWorldId))
                {
                    Console.WriteLine($"Sending order to TechWorld with ProductId: {productMapping.TechWorldId}");
                    tasks.Add(SendOrderToDistributorAsync(
                        "http://localhost:5213/api/OrderConfirmation",
                        order,
                        int.Parse(productMapping.TechWorldId),
                        "TechWorld"
                    ));
                }

                // Send to GadgetCentral (Port 5019)
                if (!string.IsNullOrEmpty(productMapping.GadgetCentralId))
                {
                    Console.WriteLine($"Sending order to GadgetCentral with ProductId: {productMapping.GadgetCentralId}");
                    tasks.Add(SendOrderToDistributorAsync(
                        "http://localhost:5019/api/OrderConfirmation",
                        order,
                        int.Parse(productMapping.GadgetCentralId),
                        "GadgetCentral"
                    ));
                }

                var results = await Task.WhenAll(tasks);
                var success = results.All(r => r);
                Console.WriteLine($"DistributorOrderService: Completed sending order {order.OrderId}. Success: {success}");
                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending order to distributors: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> SendOrderToDistributorAsync(string url, Order order, int distributorProductId, string distributorName)
        {
            try
            {
                // Create order data with distributor-specific ProductId
                var distributorOrderData = new
                {
                    // Don't set OrderId - let the database generate it
                    RequestId = order.QuotationRequestId,
                    ConfirmationDate = order.OrderDate,
                    Status = order.Status,
                    UserId = order.UserId,
                    ProductId = distributorProductId, // Use distributor-specific ID
                    Stock = order.Stock,
                    QuotationRequestId = order.QuotationRequestId,
                    QuotationResponseId = order.QuotationResponseId
                };

                var response = await _httpClient.PostAsJsonAsync(url, distributorOrderData);
                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Order successfully sent to {distributorName}");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to send order to {distributorName}: {response.StatusCode} - {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending order to {distributorName}: {ex.Message}");
                return false;
            }
        }
    }
} 