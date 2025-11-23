using Gadget_CentralApi.DTO;
using Gadget_CentralApi.Models;
using Gadget_CentralApi.Repo;
using Gadget_CentralApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace Gadget_CentralApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoRequestController : ControllerBase
    {
        private readonly IQuotationService _quotationService;
        private readonly IQuotationRequestRepo _quotationRequestRepo;
        private readonly IMapper _mapper;

        public QuoRequestController(IQuotationService quotationService, IQuotationRequestRepo quotationRequestRepo, IMapper mapper)
        {
            _quotationService = quotationService ?? throw new ArgumentNullException(nameof(quotationService));
            _quotationRequestRepo = quotationRequestRepo ?? throw new ArgumentNullException(nameof(quotationRequestRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> PostQuotationRequest([FromBody] QuotationRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Quotation request cannot be null.");
            }

            try
            {
                var responseDto = await _quotationService.ProcessQuotationRequestAsync(requestDto);
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} | Inner: {ex.InnerException.Message}"
                    : ex.Message;
                return StatusCode(500, $"Internal server error: {errorMessage}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuotationRequest(int id)
        {
            var request = await _quotationRequestRepo.GetByIdAsync(id);
            if (request == null)
                return NotFound();
            var dto = _mapper.Map<QuotationRequestDto>(request);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuotationRequests()
        {
            var requests = await _quotationRequestRepo.GetAllAsync();
            var dtos = requests.Select(r => _mapper.Map<QuotationRequestDto>(r)).ToList();
            return Ok(dtos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuotationRequest(int id, [FromBody] QuotationRequestDto dto)
        {
            if (dto == null)
                return BadRequest();
            var request = await _quotationRequestRepo.GetByIdAsync(id);
            if (request == null)
                return NotFound();
            _mapper.Map(dto, request);
            await _quotationRequestRepo.UpdateAsync(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotationRequest(int id)
        {
            var request = await _quotationRequestRepo.GetByIdAsync(id);
            if (request == null)
                return NotFound();
            await _quotationRequestRepo.DeleteAsync(request);
            return NoContent();
        }
    }
}