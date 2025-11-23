using System.Text;
using System.Text.Json;
using TechWorld.DTO;

namespace TechWorld.Services
{
    public interface IGadgetHubNotificationService
    {
        Task NotifyGadgetHubAsync(QuotationResponseDto responseDto);
    }

    public class GadgetHubNotificationService : IGadgetHubNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _gadgetHubApiUrl = "http://localhost:5254";

        public GadgetHubNotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task NotifyGadgetHubAsync(QuotationResponseDto responseDto)
        {
            try
            {
                                 // Convert the distributor response to the format expected by GadgetHubApi
                 var firstProduct = responseDto.ProductQuotes?.FirstOrDefault();
                 int productId = 0;
                 
                 if (firstProduct != null && !string.IsNullOrEmpty(firstProduct.ProductId))
                 {
                     if (int.TryParse(firstProduct.ProductId, out int parsedId))
                     {
                         productId = parsedId;
                     }
                     else
                     {
                         Console.WriteLine($"Warning: Could not parse ProductId '{firstProduct.ProductId}' as integer, using 0 as fallback");
                         productId = 0;
                     }
                 }
                 
                 var distributorResponse = new
                 {
                     UserId = 1, // Default user ID, you might want to get this from the request
                     ProductId = productId,
                     Price = firstProduct?.PricePerUnit ?? 0,
                     Stock = firstProduct?.AvailableQuantity ?? 0,
                     QuotationRequestId = responseDto.RequestId,
                     QuotationResponseId = responseDto.RequestId, // Using RequestId as ResponseId for now
                     Status = "Available"
                 };

                var json = JsonSerializer.Serialize(distributorResponse);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_gadgetHubApiUrl}/api/Quotation/store-response", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to notify GadgetHub: {response.StatusCode} - {errorContent}");
                }
                else
                {
                    Console.WriteLine("Successfully notified GadgetHub of quotation response");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error notifying GadgetHub: {ex.Message}");
            }
        }
    }
} 