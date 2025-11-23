using Microsoft.AspNetCore.Mvc;
using ElectroComApi.DTO;
using ElectroComApi.Services;
using ElectroComApi.Models;
using ElectroComApi.Data;
using ElectroComApi.Repo;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ElectroComApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoResponseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuotationService _quotationService;
        private readonly IQuotationResponseRepo _quotationResponseRepo;
        private readonly IMapper _mapper;
        private readonly IGadgetHubNotificationService _gadgetHubNotificationService;

        public QuoResponseController(IQuotationService quotationService, ApplicationDbContext context, IQuotationResponseRepo quotationResponseRepo, IMapper mapper, IGadgetHubNotificationService gadgetHubNotificationService)
        {
            _quotationService = quotationService ?? throw new ArgumentNullException(nameof(quotationService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _quotationResponseRepo = quotationResponseRepo ?? throw new ArgumentNullException(nameof(quotationResponseRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _gadgetHubNotificationService = gadgetHubNotificationService ?? throw new ArgumentNullException(nameof(gadgetHubNotificationService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuotationResponses()
        {
            try
            {
                var responses = await _quotationResponseRepo.GetAllAsync();
                var dtos = responses.Select(q => _mapper.Map<QuotationResponseDto>(q)).ToList();
                return Ok(new { success = true, data = dtos, count = dtos.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Failed to retrieve quotation responses", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuotationResponse(int id)
        {
            try
            {
                var response = await _quotationResponseRepo.GetByIdAsync(id);
                if (response == null)
                    return NotFound(new { success = false, error = "Quotation response not found" });
                
                var dto = _mapper.Map<QuotationResponseDto>(response);
                return Ok(new { success = true, data = dto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Failed to retrieve quotation response", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuotationResponse([FromBody] QuotationResponseDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { success = false, error = "Request body cannot be null" });

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, error = "Invalid model state", details = ModelState });
                }

                var entity = _mapper.Map<QuotationResponse>(dto);
                await _quotationResponseRepo.SaveAsync(entity);
                
                // Notify GadgetHubApi about the new quotation response
                await _gadgetHubNotificationService.NotifyGadgetHubAsync(dto);
                
                var createdDto = _mapper.Map<QuotationResponseDto>(entity);
                return CreatedAtAction(nameof(GetQuotationResponse), new { id = entity.Id }, new { success = true, data = createdDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Failed to create quotation response", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuotationResponse(int id, [FromBody] QuotationResponseDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { success = false, error = "Request body cannot be null" });

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, error = "Invalid model state", details = ModelState });
                }

                var entity = await _quotationResponseRepo.GetByIdAsync(id);
                if (entity == null)
                    return NotFound(new { success = false, error = "Quotation response not found" });
                
                _mapper.Map(dto, entity);
                await _quotationResponseRepo.UpdateAsync(entity);
                
                return Ok(new { success = true, message = "Quotation response updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Failed to update quotation response", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotationResponse(int id)
        {
            try
            {
                var entity = await _quotationResponseRepo.GetByIdAsync(id);
                if (entity == null)
                    return NotFound(new { success = false, error = "Quotation response not found" });
                
                await _quotationResponseRepo.DeleteAsync(entity);
                return Ok(new { success = true, message = "Quotation response deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Failed to delete quotation response", details = ex.Message });
            }
        }

        [HttpGet("by-request/{requestId}")]
        public async Task<IActionResult> GetQuotationResponsesByRequest(int requestId)
        {
            try
            {
                var responses = await _quotationResponseRepo.GetAllAsync();
                var filteredResponses = responses.Where(r => r.RequestId == requestId).ToList();
                var dtos = filteredResponses.Select(q => _mapper.Map<QuotationResponseDto>(q)).ToList();
                
                return Ok(new { success = true, data = dtos, count = dtos.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Failed to retrieve quotation responses by request", details = ex.Message });
            }
        }

        [HttpGet("recent/{count}")]
        public async Task<IActionResult> GetRecentQuotationResponses(int count = 10)
        {
            try
            {
                if (count <= 0 || count > 100)
                {
                    return BadRequest(new { success = false, error = "Count must be between 1 and 100" });
                }

                var responses = await _quotationResponseRepo.GetAllAsync();
                var recentResponses = responses.OrderByDescending(r => r.ResponseDate).Take(count).ToList();
                var dtos = recentResponses.Select(q => _mapper.Map<QuotationResponseDto>(q)).ToList();
                
                return Ok(new { success = true, data = dtos, count = dtos.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Failed to retrieve recent quotation responses", details = ex.Message });
            }
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetQuotationResponseStatistics()
        {
            try
            {
                var responses = await _quotationResponseRepo.GetAllAsync();
                var totalCount = responses.Count();
                var averagePrice = responses.SelectMany(r => r.ProductQuoteInfos).Average(pq => pq.PricePerUnit);
                var totalQuantity = responses.SelectMany(r => r.ProductQuoteInfos).Sum(pq => pq.AvailableQuantity);
                var uniqueRequests = responses.Select(r => r.RequestId).Distinct().Count();

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        totalCount,
                        averagePrice = Math.Round(averagePrice, 2),
                        totalQuantity,
                        uniqueRequests
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Failed to retrieve statistics", details = ex.Message });
            }
        }
    }
}