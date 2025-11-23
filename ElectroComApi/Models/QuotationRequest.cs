using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectroComApi.Models
{
    public class QuotationRequest
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; } // Changed to int as per requirement
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public List<ProductRequestInfo> ProductRequestInfos { get; set; } = new List<ProductRequestInfo>();
    }
}