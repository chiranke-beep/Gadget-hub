using System;
using System.Collections.Generic;

namespace Gadget_CentralApi.Models
{
    public class QuotationResponse
    {
        public int Id { get; set; }
        public int RequestId { get; set; } // Changed to int

        public string DistributerId { get; set; } = "3"; // Default to "1" for the first distributor
        public DateTime ResponseDate { get; set; } = DateTime.UtcNow;

        public List<ProductQuoteInfo> ProductQuoteInfos { get; set; } = new List<ProductQuoteInfo>();
    }
}