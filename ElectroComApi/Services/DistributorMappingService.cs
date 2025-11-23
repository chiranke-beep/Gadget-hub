using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;

namespace ElectroComApi.Services
{
    public interface IDistributorMappingService
    {
        Task<string> GetDistributorIdAsync(string gadgetHubProductId, string distributorType);
    }

    public class DistributorMappingService : IDistributorMappingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _gadgetHubApiUrl = "http://localhost:5254"; // GadgetHub API URL

        public DistributorMappingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDistributorIdAsync(string gadgetHubProductId, string distributorType)
        {
            try
            {
                var url = $"{_gadgetHubApiUrl}/api/DistributorMap/product/{gadgetHubProductId}";
                Console.WriteLine($"Calling GadgetHub API: {url}");
                
                // Call GadgetHub API to get the distributor mapping by GadgetHubId
                var response = await _httpClient.GetAsync(url);
                
                Console.WriteLine($"Response status: {response.StatusCode}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response content: {jsonString}");
                    
                    var mapping = JsonSerializer.Deserialize<DistributorMappingResponse>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (mapping != null)
                    {
                        var distributorId = distributorType.ToLower() switch
                        {
                            "electrocom" => mapping.ElectroComId ?? "301", // Default fallback
                            "techworld" => mapping.TechWorldId ?? "101",   // Default fallback
                            "gadgetcentral" => mapping.GadgetCentralId ?? "201", // Default fallback
                            _ => "301" // Default to ElectroCom
                        };
                        
                        Console.WriteLine($"Mapped distributor ID for {distributorType}: {distributorId}");
                        return distributorId;
                    }
                    else
                    {
                        Console.WriteLine("Failed to deserialize mapping response");
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API call failed: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't throw - use default value
                Console.WriteLine($"Error getting distributor mapping: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            // Default fallback values
            var fallbackId = distributorType.ToLower() switch
            {
                "electrocom" => "301",
                "techworld" => "101",
                "gadgetcentral" => "201",
                _ => "301"
            };
            
            Console.WriteLine($"Using fallback ID for {distributorType}: {fallbackId}");
            return fallbackId;
        }
    }

    public class DistributorMappingResponse
    {
        public int Id { get; set; }
        public int GadgetHubId { get; set; }
        public string? TechWorldId { get; set; }
        public string? GadgetCentralId { get; set; }
        public string? ElectroComId { get; set; }
    }
} 