using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using GadgetHub.DTOs;

namespace GadgetHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationRequestController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public QuotationRequestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> RequestQuotations([FromBody] List<ProductRequestDto> productRequests)
        {
            // Example URLs - replace with actual distributor endpoints
            var distributorUrls = new[]
            {
                "http://localhost:5258/api/quotations",
                "http://localhost:5019/api/quotations",
                "http://localhost:5213/api/quotations"
            };

            var results = new List<QuotationResponseDto>();

            foreach (var url in distributorUrls)
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(url, productRequests);
                if (response.IsSuccessStatusCode)
                {
                    var quote = await response.Content.ReadFromJsonAsync<QuotationResponseDto>();
                    if (quote != null)
                        results.Add(quote);
                }
            }

            return Ok(results);
        }
    }
}
