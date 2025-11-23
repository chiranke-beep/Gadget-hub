using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gadget_CentralApi.DTO
{
    public class QuotationResponseDto
    {
        [Required]
        public int RequestId { get; set; }
        
        public DateTime? ResponseDate { get; set; }
        
        [Required]
        [StringLength(50)]
        public string DistributerId { get; set; } = "3"; // Gadget Central ID
        
        [Required]
        public List<ProductQuoteInfoReadDto> ProductQuotes { get; set; } = new();
    }

    public class ProductQuoteInfoReadDto
    {
        [Required]
        [StringLength(50)]
        public string ProductId { get; set; } = string.Empty;
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal PricePerUnit { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Available quantity must be non-negative")]
        public int AvailableQuantity { get; set; }
        
        [Required]
        [Range(1, 365, ErrorMessage = "Estimated delivery days must be between 1 and 365")]
        public int EstimatedDeliveryDays { get; set; }
    }
}