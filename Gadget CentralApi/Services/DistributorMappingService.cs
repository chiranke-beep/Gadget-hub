using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;

namespace Gadget_CentralApi.Services
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
                // Call GadgetHub API to get the distributor mapping by GadgetHubId
                var response = await _httpClient.GetAsync($"{_gadgetHubApiUrl}/api/DistributorMap/product/{gadgetHubProductId}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var mapping = JsonSerializer.Deserialize<DistributorMappingResponse>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (mapping != null)
                    {
                        return distributorType.ToLower() switch
                        {
                            "electrocom" => mapping.ElectroComId ?? "301", // Default fallback
                            "techworld" => mapping.TechWorldId ?? "101",   // Default fallback
                            "gadgetcentral" => mapping.GadgetCentralId ?? "201", // Default fallback
                            _ => "201" // Default to GadgetCentral
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't throw - use default value
                Console.WriteLine($"Error getting distributor mapping: {ex.Message}");
            }

            // Default fallback values
            return distributorType.ToLower() switch
            {
                "electrocom" => "301",
                "techworld" => "101",
                "gadgetcentral" => "201",
                _ => "201"
            };
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