using System;
using System.Collections.Generic;

namespace ElectroComApi.Models
{
    public class QuotationResponse
    {
        public int Id { get; set; }
        public int RequestId { get; set; } // Changed to int

        public string DistributerId { get; set; } = "2"; // Default to "1" for the first distributor
        public DateTime ResponseDate { get; set; } = DateTime.UtcNow;

        public List<ProductQuoteInfo> ProductQuoteInfos { get; set; } = new List<ProductQuoteInfo>();
    }
}