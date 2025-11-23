using System;
using System.Collections.Generic;

namespace GadgetHub.DTOs
{
    public class QuotationResponseDto
    {
        public int RequestId { get; set; } // Changed to int
        public DateTime? ResponseDate { get; set; }
        public string DistributerId { get; set; }
        public List<ProductQuoteInfoReadDto> ProductQuotes { get; set; } = new();
    }
}
