using System;
using System.Collections.Generic;

namespace TechWorld.DTO
{
    public class QuotationRequestDto
    {
        public int RequestId { get; set; } // Changed to int
        public DateTime? RequestDate { get; set; }
        public List<ProductRequestDto> ProductRequests { get; set; } = new();
    }
}